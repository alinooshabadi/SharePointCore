using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using SharePointCore.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePointCore.Extensions
{
    public static class SPListItemExtensions
    {
        public static IEnumerable<string> GetAttachmentUrls(this SPListItem item)
        {
            return from string fileName in item.Attachments
                   orderby fileName
                   select SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);
        }

        public static bool SetGroupPermission(this SPListItem item, string groupName, string permissionLevel)
        {
            var isSet = false;
            try
            {
                item.Web.AllowUnsafeUpdates = true;
                if (!item.HasUniqueRoleAssignments)
                {
                    item.BreakRoleInheritance(false, false);
                }
                item.Update();
                item.Web.Update();

                var roleAssignment = new SPRoleAssignment(item.Web.SiteGroups[groupName]);
                roleAssignment.RoleDefinitionBindings.Add(item.Web.RoleDefinitions[permissionLevel]);
                item.RoleAssignments.Add(roleAssignment);
                item.Update();
                item.Web.Update();
                isSet = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"SharePointCore: Error on {nameof(SetGroupPermission)}", ex);
            }
            return isSet;
        }

        public static bool SetUserPermission(this SPListItem item, string userName, string permissionLevel)
        {
            var isSet = false;
            try
            {
                item.Web.AllowUnsafeUpdates = true;
                if (!item.HasUniqueRoleAssignments)
                {
                    item.BreakRoleInheritance(false, false);
                }
                item.Update();
                item.Web.Update();

                var roleAssignment = new SPRoleAssignment(item.Web.AllUsers[userName]);
                roleAssignment.RoleDefinitionBindings.Add(item.Web.RoleDefinitions[permissionLevel]);
                item.RoleAssignments.Add(roleAssignment);
                item.Update();
                item.ParentList.Update();
                item.Web.Update();
                isSet = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"SharePointCore: Error on {nameof(SetUserPermission)}", ex);
            }
            return isSet;
        }

        public static string TryGetFieldValue(this SPListItem listItem, string fieldName)
        {
            var fieldValue = string.Empty;
            try
            {
                if (listItem.ParentList.TryGetField(fieldName).Type == SPFieldType.Lookup)
                {
                    if ((listItem.ParentList.TryGetField(fieldName) as SPFieldLookup).AllowMultipleValues)
                    {
                        var lookupValue = new SPFieldLookupValueCollection(listItem[fieldName].ToStringSafe());
                        var values = lookupValue.Select(x => x.LookupValue).ToArray().ToListString();

                        fieldValue = values;
                    }
                    else
                    {
                        fieldValue = new SPFieldLookupValue(listItem[fieldName].ToStringSafe()).LookupValue;
                    }
                }
                else if (listItem.ParentList.TryGetField(fieldName).Type == SPFieldType.User)
                {
                    if ((listItem.ParentList.TryGetField(fieldName) as SPFieldUser).AllowMultipleValues)
                    {
                        var lookupValue = new SPFieldLookupValueCollection(listItem[fieldName].ToStringSafe());
                        var values = lookupValue.Select(x => x.LookupValue).ToArray().ToListString();

                        fieldValue = values;
                    }
                    else
                    {
                        fieldValue = new SPFieldLookupValue(listItem[fieldName].ToStringSafe()).LookupValue;
                    }
                }
                else if (listItem.ParentList.TryGetField(fieldName).Type == SPFieldType.MultiChoice)
                {
                    var choices = new SPFieldMultiChoiceValue(listItem[fieldName].ToString());
                    var chiocesList = new List<string>();
                    for (int i = 0; i < choices.Count; i++)
                    {
                        chiocesList.Add(choices[i]);
                    }
                    return chiocesList.ToArray().ToListString();
                }
                else if (listItem.ParentList.TryGetField(fieldName).Type == SPFieldType.Calculated)
                {
                    var cf = (SPFieldCalculated)listItem.Fields[fieldName];
                    var value = cf.GetFieldValueForEdit(listItem[fieldName]);
                    fieldValue = value;
                }
                else
                {
                    fieldValue = listItem[fieldName].ToStringSafe();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"SharePointCore: Error on {nameof(TryGetFieldValue)}", ex);
            }
            return fieldValue;
        }

        public static int TryGetLookupId(this SPListItem listItemName, string fieldName)
        {
            var lookupId = 0;
            try
            {
                if (listItemName.ParentList.TryGetField(fieldName).Type == SPFieldType.Lookup && listItemName.TryGetFieldValue(fieldName) != null)
                {
                    var fieldLookupValue = new SPFieldLookupValue(listItemName[listItemName.ParentList.TryGetField(fieldName).Id].ToStringSafe());
                    lookupId = fieldLookupValue.LookupId;
                }
                else
                {
                    lookupId = 0;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw new Exception($"SharePointCore: Error on {nameof(TryGetLookupId)}", ex);
            }
            return lookupId;
        }
    }
}