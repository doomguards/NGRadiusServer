using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NGRadius.Core;
using System.Security.Cryptography;


namespace NGRadius.Core
{
    public class RadiusPaket
    {
        private RadiusPaket()
        {
            Attributes = new List<RadiusAttribute>();
        }
        public byte Code { get; set; }
        public byte Id { get; set; }
        public UInt16 Length { get; set; }
        public byte[] Authenticator{get;set;}
        public byte[] Paket { get; set; }
        public List<RadiusAttribute> Attributes { get; set; }
        public static RadiusPaket Parser(byte[] receiveData)
        {
            var paket = new RadiusPaket();

            if (receiveData.Length < 20) throw new Exception("包长度小于20!");
            byte code = receiveData[0];
            byte id = receiveData[1];

            UInt16 len = BitConverter.ToUInt16(new byte[] { receiveData[3], receiveData[2] }, 0);
            if (len != receiveData.Length) throw new Exception("包长度异常!");

            var authenticator = new byte[16];
            Array.Copy(receiveData, 4, authenticator, 0, 16);

            paket.Code = code;
            paket.Id = id;
            paket.Length = len;
            paket.Authenticator = authenticator;
            paket.Paket = new byte[receiveData.Length];
            Array.Copy(receiveData, paket.Paket, paket.Length);
            //提取属性
            var index = 20;
    
            while (index < len)
            {
                var attrType = receiveData[index];
                var attrLen = receiveData[index + 1];
                var attrValue = new byte[attrLen - 2];
                Array.Copy(receiveData, index + 2, attrValue, 0, attrValue.Length);
                var attr = new RadiusAttribute(attrType, attrValue);
                index += attrLen;
                paket.Attributes.Add(attr);

            }

            return paket;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sharedSecret"></param>
        /// <param name="requestAuthernticator"></param>
        /// <param name="code">2:accept,3:reject</param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RadiusPaket Build(string  sharedSecret,byte[] requestAuthernticator,byte code, byte id)
        {
           var attributes = new List<RadiusAttribute>();

           var sessionTimeoutAttr = new RadiusAttribute(27, new byte[] {  00, 0x98, 0x96, 0x7F });
           attributes.Add(sessionTimeoutAttr);


            var msgAuth= GenMessageAuthenticator(sharedSecret, requestAuthernticator, code, id, attributes);
            attributes.Add(msgAuth);



            //20个字节加，属性长度，加MessageAuthenticator 18字节
            UInt16 len = (ushort)(20 + attributes.Sum(ent => ent.Paket.Length));
            var lenBytes = BitConverter.GetBytes((ushort)len).Reverse();

            #region 计算Response Authernticator,
            var authRaw = new List<byte>();
            authRaw.Add(code);
            authRaw.Add(id);
            authRaw.AddRange(lenBytes);
            authRaw.AddRange(requestAuthernticator);
            foreach (var a in attributes)
            {
                authRaw.AddRange(a.Paket);
            }
            authRaw.AddRange(Encoding.Default.GetBytes(sharedSecret));
            var authernticator =MD5.Create("MD5").ComputeHash(authRaw.ToArray());
            #endregion


           
            var paketBytes = new List<byte>();
            paketBytes.Add(code);
            paketBytes.Add(id);
            paketBytes.AddRange(lenBytes);
            paketBytes.AddRange(authernticator);
            foreach (var a in attributes)
            {
                paketBytes.AddRange(a.Paket);

            }

            var paket = new RadiusPaket();
            paket.Attributes = attributes;
            paket.Authenticator = authernticator;
            paket.Code = code;
            paket.Id = id;
            paket.Length = len;
            paket.Paket = paketBytes.ToArray();
            return paket;
        }
        public static  string ToHexStr(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }
        public static String ToHexStr(byte b)
        {
            return BitConverter.ToString(new byte[] { b }).Replace("-", "");
        }

        #region Util
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwdAttrPaket">User-Password段，包括type跟length+x...</param>
        /// <param name="SharedSecret"></param>
        /// <param name="RequestAuthenticator"></param>
        /// <returns></returns>
        public static byte[] EncodePAPPwd(String pwdStr, string SharedSecret, byte[] RequestAuthenticator)
        {



            var pwdBytes = Encoding.Default.GetBytes(pwdStr);
            var dataLen = pwdBytes.Length / 16;
            var r = pwdBytes.Length % 16;
            if (r != 0)
            {
                dataLen++;
            }

            var pArr = new byte[dataLen * 16];
            Array.Copy(pwdBytes, pArr, pwdBytes.Length);

            //补0字节处理
            if (r != 0)
            {
                for (int i = pwdBytes.Length; i < pArr.Length; i++)
                {
                    pArr[i] = 0;
                }
            }

            var bi = new byte[16];
            var ciArr = new byte[pArr.Length];

            var shareSecretBytes = Encoding.Default.GetBytes(SharedSecret);

            var tmp = new byte[shareSecretBytes.Length + 16];
            Array.Copy(shareSecretBytes, tmp, shareSecretBytes.Length);
            Array.Copy(RequestAuthenticator, 0, tmp, shareSecretBytes.Length, 16);
            Array.Copy(MD5.Create("MD5").ComputeHash(tmp), bi, 16);


            for (int i = 0; i < dataLen; i++)
            {
                for (int bIndex = 0; bIndex < 16; bIndex++)
                {
                    ciArr[i * 16 + bIndex] = (byte)(bi[bIndex] ^ pArr[i * 16 + bIndex]);
                }

                Array.Copy(ciArr, i * 16, tmp, shareSecretBytes.Length, 16);
                Array.Copy(MD5.Create("MD5").ComputeHash(tmp), bi, 16);

            }
            return ciArr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pwdAttrPaket">User-Password段，包括type跟length+x...</param>
        /// <param name="SharedSecret"></param>
        /// <param name="RequestAuthenticator"></param>
        /// <returns></returns>
        public static byte[] DecodePAPPwd(byte[] pwdAttrPaket, string SharedSecret, byte[] RequestAuthenticator)
        {
            var chunksCount = (pwdAttrPaket.Length - 2) / 16;
            var biArr = new byte[pwdAttrPaket.Length - 2];

            var shareSecretBytes = Encoding.Default.GetBytes(SharedSecret);
            var tmp = new byte[shareSecretBytes.Length + 16];
            Array.Copy(shareSecretBytes, tmp, shareSecretBytes.Length);
            Array.Copy(RequestAuthenticator, 0, tmp, shareSecretBytes.Length, 16);
            Array.Copy(MD5.Create("MD5").ComputeHash(tmp), biArr, 16);


            for (int i = 1; i < chunksCount; i++)
            {

                Array.Copy(pwdAttrPaket, ((i - 1) * 16) + 2, tmp, shareSecretBytes.Length, 16);
                Array.Copy(MD5.Create("MD5").ComputeHash(tmp), 0, biArr, i * 16, 16);
            }

            for (int i = 0; i < biArr.Length; i++)
            {
                biArr[i] = (byte)(biArr[i] ^ pwdAttrPaket[2 + i]);
            }

            return biArr;
        }

        public static RadiusAttribute GenMessageAuthenticator(string  sharedSecret, byte[] requestAuthenticator, byte code, byte id, List<RadiusAttribute> attributes)
        {

            if (attributes == null) attributes = new List<RadiusAttribute>();

            var attr = new RadiusAttribute(80, new byte[16] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0});
            
            var msgAuthRaw = new List<byte>();
            msgAuthRaw.Add(code);
            msgAuthRaw.Add(id);
            //20个字节加，属性长度，加MessageAuthenticator 18字节
            UInt16 len = (ushort)(20 + attributes.Sum(ent => ent.Paket.Length) + 18);
            msgAuthRaw.AddRange(BitConverter.GetBytes((ushort)len).Reverse() );
            msgAuthRaw.AddRange(requestAuthenticator);
            foreach (var a in attributes)
            {
                msgAuthRaw.AddRange(a.Paket);
            }
            msgAuthRaw.AddRange(attr.Paket);


            var hmacMD5 = HMACMD5.Create("HMACMD5");

            hmacMD5.Key = Encoding.Default.GetBytes(sharedSecret);
            var hmacBytes = hmacMD5.ComputeHash(msgAuthRaw.ToArray());
            //更新属性内容
            for (int i = 0; i < 16; i++)
            {
                attr.Value[i] = hmacBytes[i];
                attr.Paket[i + 2] = hmacBytes[i];
            }
            return attr;
        }

        #endregion
    }
}
