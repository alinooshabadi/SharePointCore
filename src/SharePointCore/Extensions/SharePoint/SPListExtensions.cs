using Microsoft.SharePoint;
using SharePointCore.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace SharePointCore.Extensions
{
    public static class SPListExtensions
    {
        public static SPView CopyView(this SPList list, SPView templateView)
        {
            try
            {
                SPView view = null;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    list.ParentWeb.AllowUnsafeUpdates = true;
                    view = list.TryGetView(templateView.Title);
                    if (view == null)
                    {
                        view = list.Views.Add(templateView.Title, templateView.ViewFields.ToStringCollection(), templateView.Query, templateView.RowLimit, templateView.Paged, templateView.DefaultView);
                        list.Update();
                    }
                    else
                    {
                        view.ViewFields.DeleteAll();
                        foreach (string field in templateView.ViewFields)
                            view.ViewFields.Add(field);

                        view.RowLimit = templateView.RowLimit;
                        view.Query = templateView.Query;
                        view.Paged = templateView.Paged;
                        view.DefaultView = templateView.DefaultView;
                        view.Aggregations = templateView.Aggregations;
                        view.Update();
                        list.Update();
                    }
                });
                return view;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public static void DeleteField(this SPList list, string fieldName)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    list.ParentWeb.AllowUnsafeUpdates = true;
                    var field = list.TryGetField(fieldName);
                    if (field != null)
                    {
                        field.Delete();
                        field.ParentList.Update();
                    }
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public static SPFieldMultiChoice EnsureChoiceField(this SPList list, string fieldName, List<string> chioces, bool isMultiSelect, bool fillInChoice = false)
        {
            SPFieldMultiChoice field = null;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    if (!list.Fields.ContainsField(fieldName))
                    {
                        if (isMultiSelect)
                            list.Fields.Add(fieldName, SPFieldType.MultiChoice, false);
                        else
                            list.Fields.Add(fieldName, SPFieldType.Choice, false);
                        list.Update();
                    }
                    field = (SPFieldMultiChoice)list.TryGetField(fieldName);
                    field.FillInChoice = fillInChoice;
                    field.Choices.Clear();
                    foreach (string str in chioces)
                    {
                        field.Choices.Add(str);
                    }
                    field.Update();
                    list.Update();
                });
                return field;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return field;
            }
        }

        public static SPField EnsureField(this SPList list, SPFieldType type, string displayName, string internalName, bool isRequired, string formula)
        {
            var field = list.EnsureField(type, displayName, internalName, isRequired);
            if (field.Type == SPFieldType.Calculated)
            {
                var strNewField = (SPFieldCalculated)field;
                strNewField.Formula = formula;
                strNewField.Update();
            }

            return field;
        }

        public static SPField EnsureField(this SPList list, SPFieldType type, string displayName, string internalName, bool isRequired, SPCalendarType calendarType)
        {
            var field = list.EnsureField(type, displayName, internalName, isRequired);
            if (field.Type == SPFieldType.DateTime)
            {
                try
                {
                    if (!list.Fields.ContainsField(internalName))
                    {
                        list.Fields.Add(internalName, SPFieldType.DateTime, false);
                        list.Update();

                        (field as SPFieldDateTime).DisplayFormat = SPDateTimeFieldFormatType.DateOnly;
                        (field as SPFieldDateTime).CalendarType = calendarType;
                        field.Update();
                    }
                    else
                    {
                        (field as SPFieldDateTime).DisplayFormat = SPDateTimeFieldFormatType.DateOnly;
                        (field as SPFieldDateTime).CalendarType = calendarType;
                        field.Update();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }
            return field;
        }

        public static SPField EnsureField(this SPList list, SPFieldType type, string displayName, string internalName, bool isRequired)
        {
            SPField field = null;
            try
            {
                list.ParentWeb.AllowUnsafeUpdates = true;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    field = list.TryGetField(displayName);
                    if (field == null)
                    {
                        list.Fields.Add(internalName, type, isRequired);
                        list.Update();
                        var datefield = list.TryGetField(internalName);
                        if (datefield.Type == SPFieldType.DateTime)
                        {
                            (datefield as SPFieldDateTime).DisplayFormat = SPDateTimeFieldFormatType.DateOnly;
                            datefield.Update();
                            list.Update();
                        }
                        field = list.TryGetField(internalName);
                        field.RenameField(displayName);
                        //field.Update();
                        if (field.Type == SPFieldType.Note)
                        {
                            var notefield = list.TryGetField(internalName);
                            var f = (SPFieldMultiLineText)list.TryGetField(internalName);
                            f.RichText = true;
                            f.RichTextMode = SPRichTextMode.FullHtml;
                            f.Update();
                            f.ParentList.Update();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return field;
        }

        public static void EnsureLookUpField(this SPList list, string fieldName, string lookupListName, string lookUpFieldName)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    if (!list.Fields.ContainsField(fieldName))
                    {
                        var lookupListID = list.ParentWeb.Lists.TryGetList(lookupListName).ID;
                        var field = list.Fields.AddLookup(fieldName, lookupListID, false);

                        var listLook = list.ParentWeb.TryGetList(lookupListName);

                        var lookUp = new SPFieldLookup(list.Fields, field);

                        lookUp.LookupField = listLook.TryGetField(lookUpFieldName).InternalName;
                        lookUp.Indexed = true;
                        lookUp.Update();
                        list.Update();
                    }
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public static void RenameList(this SPList list, string listName)
        {
            try
            {
                CultureInfo originalUICulture = Thread.CurrentThread.CurrentUICulture;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    if (SPContext.Current == null)
                    {
                        Thread.CurrentThread.CurrentUICulture =
                            new CultureInfo((int)list.ParentWeb.Language);
                    }
                    list.Title = listName;
                    list.Update();

                    Thread.CurrentThread.CurrentUICulture = originalUICulture;
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public static SPField TryGetField(this SPList list, string fieldName)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            SPField field = null;
            try
            {
                if (list.Fields.ContainsFieldWithStaticName(fieldName))
                {
                    field = list.Fields.GetFieldByInternalName(fieldName);
                }
                else
                {
                    field = list.Fields[fieldName];
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return field;
        }

        public static SPView TryGetView(this SPList list, string viewName)
        {
            try
            {
                return list.Views[viewName];
            }
            catch
            {
                return null;
            }
        }

        public static void UnrequiredAndHideField(this SPList list, string fieldName)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    list.ParentWeb.AllowUnsafeUpdates = true;
                    var field = list.TryGetField(fieldName);
                    field.Required = false;
                    field.Hidden = true;
                    field.ShowInDisplayForm = false;
                    field.ShowInEditForm = false;
                    field.ShowInNewForm = false;
                    field.ShowInViewForms = false;
                    field.Update();
                }
             );
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public static SPView UpdateDefaultView(this SPList list, string ViewName, List<string> SortedViewFields)
        {
            try
            {
                SPView view = null;
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    list.ParentWeb.AllowUnsafeUpdates = true;

                    // get the view instance by view display name
                    view = list.Views[ViewName];

                    //Delete all existing fields
                    view.ViewFields.DeleteAll();

                    // add fields to the view
                    foreach (var field in SortedViewFields)
                        view.ViewFields.Add(field);

                    // update view for new fields
                    view.Update();
                    list.Update();
                });
                return view;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return null;
            }
        }

        public static void SetPermission(this SPList list, string roleDefinitionName, SPPrincipal principal)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate ()
                {
                    var roleDefinition = list.ParentWeb.RoleDefinitions[roleDefinitionName];

                    var roleAssignment = new SPRoleAssignment(principal);
                    roleAssignment.RoleDefinitionBindings.Add(roleDefinition);
                    list.RoleAssignments.Add(roleAssignment);
                    list.Update();
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }
    }
}