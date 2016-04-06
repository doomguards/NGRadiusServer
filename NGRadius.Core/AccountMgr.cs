using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using F.Studio.Common.Cfg;
using System.Web.Caching;
namespace NGRadius.Core
{
    public class AccountMgr
    {
        public static readonly AccountMgr Instance = new AccountMgr();

        private AccountMgr() { }

        const String USER_CACHE_KEY = "user_name_cache_3ns;3";
        public String[] Users =null;

        public  bool CheckUser(string username)
        {
            Users = HttpRuntime.Cache[USER_CACHE_KEY] as String[];
            if (Users == null)
            {
                try
                {
                    using (var ctx = DBCtx.GetCtx())
                    {
                        Users = ctx.tbUsers.Select(ent => ent.username).OrderBy(ent => ent).ToArray();
                        var cacheTime = SimpleCfgMgr.GetV<int>("RefreshSeconds", 60);
                        HttpRuntime.Cache.Add(USER_CACHE_KEY, Users, null, DateTime.Now.AddSeconds(cacheTime),
                            Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                  
                            LogUtil.Debug("缓存刷新:" + DateTime.Now);
                     
                    }
                }
                catch { }
            }
            if (Users == null || Users.Length <= 0) return false;
           return  Array.BinarySearch<string>(Users, username) >=0;
        }
    }
    
}
