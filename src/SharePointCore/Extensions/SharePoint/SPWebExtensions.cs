﻿using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using SharePointCore.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharePointCore.Extensions
{
    public static class SPWebExtensions
    {
        public static SPList TryGetList(this SPWeb web, string listName)
        {
            SPList list = null;
            try
            {
                list = web.Lists[listName];
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"SharePointCore: Error on {nameof(TryGetList)}", ex);
            }
            return list;
        }

        public static SPList EnsureList(this SPWeb web, string displayName, string internalName)
        {
            SPList list = null;
            var originalUICulture = Thread.CurrentThread.CurrentUICulture;

            if (SPContext.Current == null)
            {
                Thread.CurrentThread.CurrentUICulture =
                    new CultureInfo((int)web.Language);
            }
            if (web.TryGetList(displayName) == null && web.TryGetList(internalName) == null)
            {
                var listGuid = web.Lists.Add(internalName, "", SPListTemplateType.GenericList);
                list = web.Lists.GetList(listGuid, true);
                list.Title = displayName;
                list.Update();
            }
            else
            {
                list = web.TryGetList(displayName) == null ? web.TryGetList(internalName) : web.TryGetList(displayName);
            }

            return list;
        }

        public static SPGroup TryGetGroup(this SPWeb web, string groupName)
        {
            SPGroup group = null;
            try
            {
                group = web.SiteGroups[groupName];
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"SharePointCore: Error on {nameof(TryGetGroup)}", ex);
            }
            return group;
        }

        public static SPUser TryGetUser(this SPWeb web, string userName)
        {
            SPUser user = null;
            try
            {
                var info = SPUtility.ResolvePrincipal(web, userName, SPPrincipalType.User, SPPrincipalSource.All, web.SiteUsers, false);
                user = web.SiteUsers[info.LoginName];
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"SharePointCore: Error on {nameof(TryGetUser)}", ex);
            }

            return user;
        }

        public static void SetPermission(this SPWeb web, string roleDefinitionName, SPPrincipal principal)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    var roleDefinition = web.ParentWeb.RoleDefinitions[roleDefinitionName];

                    var roleAssignment = new SPRoleAssignment(principal);
                    roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                    web.RoleAssignments.Add(roleAssignment);
                    web.Update();
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"SharePointCore: Error on {nameof(SetPermission)}", ex);
            }
        }
    }
}