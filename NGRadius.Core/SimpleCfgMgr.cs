using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web;
using System.Web.Caching;
using System.Reflection;
using System.IO;
namespace F.Studio.Common.Cfg
{
    public class SimpleCfgMgr
    {
        private static readonly SimpleCfgMgr _Instance = new SimpleCfgMgr();
        /// <summary>
        /// 需要提供默认配置文件SConfig.txt
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static T GetV<T>(string key, T def)
        {
            return _Instance.GetCfg<T>(key, def);
        }

        /// <summary>
        /// 提供配置文件名称,不包括路径
        /// 配置文件跟可执行文件需要在同一目录下
        /// 内容是
        /// key=value 这样一行一个配置
        /// </summary>
        /// <param name="configFilename"></param>
        public SimpleCfgMgr(string configFilename)
        {
            this.ConfigFileanme = configFilename;
            this.CacheKey = "SimpleCfgMgr_" + Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 使用Sconfig.txt配置文件
        /// </summary>
        private SimpleCfgMgr()
        {

        }

        private string ConfigFileanme = "SConfig.txt";
        private string _CacheKey = "F.Studio.Common.Cfg.SimpleCfgMgr";
        public string CacheKey
        {

            get
            {

                return _CacheKey;
            }
            private set
            {
                _CacheKey = value;
            }
        }

        private string FileName
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileanme);
            }


        }
        private List<CfgRawItem> RawList
        {
            get
            {
                var rawList = HttpRuntime.Cache[CacheKey] as List<CfgRawItem>;
                if (rawList == null)
                {

                    rawList = ParseContent();
                    var monitorFilename = FileName;
                    try
                    {
                        var fileCacheDep = new CacheDependency(monitorFilename);
                        System.Web.HttpRuntime.Cache.Add(CacheKey, rawList, fileCacheDep, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.High, null);
                    }
                    catch (Exception) { };
                    //Console.WriteLine("读取文件!");

                }


                return rawList;

            }


        }


        /// <summary>
        /// 读取器
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defV"></param>
        /// <returns></returns>
        public T GetCfg<T>(string key, T defV)
        {
            var list = RawList;
            if (list == null || list.Count == 0) return defV;

            foreach (var item in RawList)
            {
                if (string.Compare(key, item.Key, true) == 0)
                {
                    return TryParser<T>(item.Value, defV);
                }
            }
            return defV;
        }
        /// <summary>
        /// 解析器
        /// </summary>
        /// <returns></returns>
        private List<CfgRawItem> ParseContent()
        {

            var txt = LoadTextContent().Trim();
            var lines = txt.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            List<CfgRawItem> list = new List<CfgRawItem>();
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line)) continue;
                var str = line.Trim();
                //忽略注释
                if (str.StartsWith("/") || str.StartsWith("-") || str.StartsWith("*") || str.StartsWith("#")) continue;
                var index = str.IndexOf("=");
                if (index > 0 && str.Length > index + 1) //等号前后至少有一个非空字符
                {
                    var key = str.Substring(0, index);
                    var value = str.Substring(index + 1, str.Length - index - 1);
                    list.Add(new CfgRawItem(key.Trim(), value.Trim()));
                }


            }

            return list;
        }
        /// <summary>
        /// 加载器
        /// </summary>
        /// <returns></returns>
        private string LoadTextContent()
        {
            if (!File.Exists(FileName))
            {
                return string.Empty;
            }
            try
            {
                var content = File.ReadAllText(FileName);
                return content;
            }
            catch (Exception)
            {
                return string.Empty;
            }


        }


        ~SimpleCfgMgr()
        {
            HttpRuntime.Cache.Remove(CacheKey);
        }


        #region HelpMethod
        protected static T TryParser<T>(object v, T dValue)
        {

            if (v == null)
            {
                return dValue;
            }
            else
            {
                T t = default(T);
                try
                {
                    if (t == null)//可空类型
                    {
                        Type type = typeof(T);

                        TypeConverter conv = TypeDescriptor.GetConverter(type);
                        t = (T)conv.ConvertFrom(v);
                    }
                    else
                    {

                        t = (T)Convert.ChangeType(v, typeof(T));
                    }
                }
                catch
                {

                    t = dValue;
                }
                return t;
            }
        }

        #endregion



    }

    #region Model
    public class CfgRawItem
    {
        public CfgRawItem(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    #endregion
}