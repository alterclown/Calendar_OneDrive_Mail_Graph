using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Calendar_OneDrive_Mail_Graph.Models
{
    public class MSALSessionCache
    {
        private static ReaderWriterLockSlim SessionLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        string UserId = string.Empty;
        string CacheId = string.Empty;
        HttpContextBase httpContext=null;
        TokenCache cache = new TokenCache();
        public MSALSessionCache(string userId,HttpContextBase httpcontext)
        {

            UserId = userId;
            CacheId = userId + "_TokenCache";
            httpContext = httpcontext;
            Load();
        }
        public TokenCache GetMsalCacheInstance()
        {
            cache.SetBeforeAccess(BeforeAccessNotification);
            cache.SetAfterAccess(AfterAccessNotification);
            Load();
            return cache;
        }
        public void SaveUserStateValue(string state) {
            SessionLock.EnterWriteLock();
            httpContext.Session[CacheId+"_state"]= state;
        }
        public string ReadUserStateValue()
        {
            string state = string.Empty;
            SessionLock.EnterReadLock();
            state = (string)httpContext.Session[CacheId + "_state"];
            SessionLock.ExitReadLock();
            return state;
        }
        void BeforeAccessNotification(TokenCacheNotificationArgs args)
        {
            Load();
        }

        // Triggered right after MSAL accessed the cache.
        void AfterAccessNotification(TokenCacheNotificationArgs args)
        {
            // if the access operation resulted in a cache update
            if (args.HasStateChanged)
            {
                Persist();
            }
        }
        public void Load() {
            SessionLock.EnterReadLock();
            byte[] blob = (byte[])httpContext.Session[CacheId];
            if (blob != null) {
                cache.Deserialize(blob);
            }
            SessionLock.ExitReadLock();
        }
        public void Persist() {
            SessionLock.EnterReadLock();
            httpContext.Session[CacheId] = cache.Serialize();
            SessionLock.ExitReadLock();
        }
    }
}