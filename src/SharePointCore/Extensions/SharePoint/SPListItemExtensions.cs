using Microsoft.SharePoint;
using SharePointCore.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharePointCore.Extensions.SharePoint
{
    public static class SPListItemExtensions
    {
        public static string TryGetFieldValue(this SPListItem listItem, string fieldName)
        {
            try
            {
                if (listItem.ParentList.TryGetField(fieldName).Type == SPFieldType.Lookup)
                {
                    if ((listItem.ParentList.TryGetField(fieldName) as SPFieldLookup).AllowMultipleValues)
                    {
                        var spflvc = new SPFieldLookupValueCollection(listItem[fieldName].ToStringSafe());
                        var values = string.Join(", ", spflvc.Select(x => x.LookupValue).ToArray());

                        return values;
                    }
                    else
                    {
                        return new SPFieldLookupValue(listItem[fieldName].ToStringSafe()).LookupValue;
                    }
                }
                else if (listItem.ParentList.TryGetField(fieldName).Type == SPFieldType.User)
                {
                    if ((listItem.ParentList.TryGetField(fieldName) as SPFieldUser).AllowMultipleValues)
                    {
                        var spflvc = new SPFieldLookupValueCollection(listItem[fieldName].ToStringSafe());
                        var values = string.Join(", ", spflvc.Select(x => x.LookupValue).ToArray());

                        return values;
                    }
                    else
                    {
                        return new SPFieldLookupValue(listItem[fieldName].ToStringSafe()).LookupValue;
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
                    return string.Join(", ", chiocesList.ToArray());
                }
                else if (listItem.ParentList.TryGetField(fieldName).Type == SPFieldType.Calculated)
                {
                    var cf = (SPFieldCalculated)listItem.Fields[fieldName];
                    var value = cf.GetFieldValueForEdit(listItem[fieldName]);
                    return value;
                }
                else
                {
                    return listItem[fieldName].ToStringSafe();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int TryGetLookupId(this SPListItem listItemName, string fieldName)
        {
            try
            {
                if (listItemName.ParentList.TryGetField(fieldName).Type == SPFieldType.Lookup && listItemName.TryGetFieldValue(fieldName) != null)
                {
                    var fieldLookupValue = new SPFieldLookupValue(listItemName[listItemName.ParentList.TryGetField(fieldName).Id].ToStringSafe());
                    return fieldLookupValue.LookupId;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static bool SetGroupPermission(this SPListItem item, string groupName, string permissionLevel)
        {
            try
            {
                item.Web.AllowUnsafeUpdates = true;
                if (!item.HasUniqueRoleAssignments)
                {
                    item.BreakRoleInheritance(false, false);
                }
                item.Update();
                item.Web.Update();

                var roleAssignment = new SPRoleAssignment((SPPrincipal)item.Web.SiteGroups[groupName]);
                roleAssignment.RoleDefinitionBindings.Add(item.Web.RoleDefinitions[permissionLevel]);
                item.RoleAssignments.Add(roleAssignment);
                item.Update();
                item.Web.Update();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }

        public static bool SetUserPermission(this SPListItem item, string userName, string permissionLevel)
        {
            try
            {
                item.Web.AllowUnsafeUpdates = true;
                if (!item.HasUniqueRoleAssignments)
                {
                    item.BreakRoleInheritance(false, false);
                }
                item.Update();
                item.Web.Update();

                var roleAssignment = new SPRoleAssignment((SPPrincipal)item.Web.AllUsers[userName]);
                roleAssignment.RoleDefinitionBindings.Add(item.Web.RoleDefinitions[permissionLevel]);
                item.RoleAssignments.Add(roleAssignment);
                item.Update();
                item.ParentList.Update();
                item.Web.Update();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
