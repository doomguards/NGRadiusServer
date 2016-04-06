/****************************************************************************************
* Class: RadiusAttribute                                                               *
****************************************************************************************
* The class RadiusAttribute is contains ONE radius-attribute in an arraylist of bytes. *
* There is also the need for an index for each Attribute, which is normally the        *
* radius-Type                                                                          *
***************************************************************************************/

/*
 * ATTRIBUTES (dynamic length):
 *  0                   1                   2
 *  0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0
 * +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
 * |      Type     |    Length     |  Value ...
 * +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
 * 
 *  - User Password = Type 2 (String)
 *  A complete Attribute-List: http://www.freeradius.org/rfc/attributes.html
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGRadius.Core
{
    public class RadiusAttribute
    {
        //******************************ATTRIBUTES******************************************
        private byte pIndex = 0;
        private string pName = null;
        private byte[] pRadiusAttribute = null;
        private byte[] pPaket = null;
        //**********************************************************************************


        //******************************PROPERTIES******************************************
        public byte AttrType { get; set; }
        public byte Index
        {
            get { return pIndex; }
        }

        public byte[] Value
        {
            set { pRadiusAttribute = value; }
            get { return pRadiusAttribute; }
        }

        public byte[] Paket
        {
            get { return pPaket; }
        }

        public string Name
        {
            get { return pName; }
        }

        //**********************************************************************************

        //******************************CONSTRUCTORS****************************************
        public RadiusAttribute()
        {
        } //public RadiusAttribute()


        public RadiusAttribute(byte Type, byte[] Value)
        {
            Assemble_Attribute(Type, Value);
        } //public RadiusAttribute(byte Type, byte[] Value)
        //**********************************************************************************


 
        private void Assemble_Attribute(byte Type, byte[] Value)
        {
            AttrType = Type;
            byte[] pThisPaket = new byte[Value.Length + 2];
            pThisPaket[0] = Type; // The Type Field
            pThisPaket[1] = Convert.ToByte(pThisPaket.Length);

            pRadiusAttribute = Value;
            Array.Copy(pRadiusAttribute, 0, pThisPaket, 2, Value.Length);

            pPaket = pThisPaket;
            pIndex = Type;
            pName = TypeToString(Type);
        } //private byte[] Assemble_Attribute(int Type, string Value) 
        #region help
        public static string TypeToString(byte Type)
        {
            string pResult = null;

            switch (Type)
            {
                case 1:
                    //http://www.freeradius.org/rfc/rfc2865.html#User-Name
                    pResult = "User-Name";
                    break;

                case 2:
                    //http://www.freeradius.org/rfc/rfc2865.html#User-Password
                    pResult = "User-Password";
                    break;

                case 3:
                    //http://www.freeradius.org/rfc/rfc2865.html#CHAP-Password
                    pResult = "CHAP-Password";
                    break;

                case 4:
                    //http://www.freeradius.org/rfc/rfc2865.html#NAS-IP-Address
                    pResult = "NAS-IP-Address";
                    break;

                case 5:
                    //http://www.freeradius.org/rfc/rfc2865.html#NAS-Port
                    pResult = "NAS-Port";
                    break;

                case 6:
                    //http://www.freeradius.org/rfc/rfc2865.html#Service-Type
                    pResult = "Service-Type";
                    break;

                case 8:
                    //http://freeradius.org/rfc/rfc2865.html#Framed-IP-Address
                    pResult = "Framed-IP-Address";
                    break;

                case 11:
                    //http://freeradius.org/rfc/rfc2865.html#Filter-Id
                    pResult = "Filter-Id";
                    break;
                case 27:
                    pResult = "Session-Timeout";
                    break;

                case 20:
                    //http://freeradius.org/rfc/rfc2865.html#Callback-Id
                    pResult = "Callback-Id";
                    break;
                case 30:
                    pResult = "Called-Station-Id";
                    break;
                case 31:
                    //http://freeradius.org/rfc/rfc2865.html#Calling-Station-Id
                    pResult = "Calling-Station-Id";
                    break;

                case 32:
                    //http://www.freeradius.org/rfc/rfc2865.html#NAS-Identifier
                    pResult = "NAS-Identifier";
                    break;
                case 61:
                    pResult = "NAS-Port-Type";
                    break;
                case 77:
                    pResult = "Connect-Info";
                    break;
                case 80:
                    pResult = " Message-Authenticator";
                    break;
                case 87:
                    //http://www.freeradius.org/rfc/rfc2869.html#NAS-Port-Id
                    pResult = "NAS-Port-ID";
                    break;

                default:
                    pResult = "unknown";
                    break;
            }

            return pResult;
        }
        #endregion     
    } //public class RadiusAttribute
    
}
