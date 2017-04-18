using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.CodeDom.Compiler;
using System.CodeDom;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace Loader
{
    /// <summary>
    /// This service resolved the types and is required when using the
    /// CodeDomHostLoader
    /// </summary>
	public class TypeResolutionService : ITypeResolutionService
	{
        //Hashtable ht = new Hashtable();

        //public TypeResolutionService()
        //{
        //}

        //public System.Reflection.Assembly GetAssembly(System.Reflection.AssemblyName name)
        //{
        //    return GetAssembly(name, true);
        //}
        //public System.Reflection.Assembly GetAssembly(System.Reflection.AssemblyName name, bool throwOnErrors)
        //{
        //    return Assembly.GetAssembly(typeof(Form));
        //}
        //public string GetPathOfAssembly(System.Reflection.AssemblyName name)
        //{
        //    return null;
        //}
        //public Type GetType(string name)
        //{
        //    return this.GetType(name, true);
        //}
        //public Type GetType(string name, bool throwOnError)
        //{
        //    return this.GetType(name, throwOnError, false);
        //}

        ///// <summary>
        ///// This method is called when dropping controls from the toolbox
        ///// to the host that is loaded using CodeDomHostLoader. For
        ///// simplicity we just go through System.Windows.Forms assembly
        ///// </summary>
        //public Type GetType(string name, bool throwOnError, bool ignoreCase)
        //{
        //    if (ht.ContainsKey(name))
        //        return (Type)ht[name];

        //    //AssemblyName[] assemblyNames = Assembly.GetAssembly(typeof(Button));
        //    //foreach (AssemblyName an in assemblyNames) 
        //    //{ 
        //    //    Assembly a = Assembly.Load(an); 
        //    //    Type[] types = a.GetTypes(); 
        //    //    foreach (Type t in types) 
        //    //    { 
        //    //        if (t.FullName == name) 
        //    //        {
        //    //            ht[name] = t;
        //    //            return t;
        //    //        } 
        //    //    } 
        //    //} 
        //    //return Type.GetType(name, throwOnError, ignoreCase);

        //    Assembly winForms = Assembly.GetAssembly(typeof(Button));
        //    Type[] types = winForms.GetTypes();
        //    //string typeName = String.Empty;
        //    foreach (Type type in types)
        //    {
        //        //typeName = "system.windows.forms." + type.Name.ToLower();
        //        if (type.FullName == name)
        //        {
        //            ht[name] = type;
        //            return type;
        //        }
        //    }
        //    return Type.GetType(name);

        //    //Type returnType = Type.GetType(name, false, ignoreCase); 
        //    //if (returnType != null) 
        //    //{ 
        //    //    return returnType; 
        //    //} 
        //    //AssemblyName[] assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies(); 
        //    //foreach (AssemblyName an in assemblyNames) 
        //    //{ 
        //    //    Assembly a = Assembly.Load(an);
        //    //    Type[] types = a.GetTypes();
        //    //    foreach (Type t in types) 
        //    //    { 
        //    //        if (t.FullName == name) 
        //    //        {
        //    //            this.ht[name] = t; 
        //    //            return t; 
        //    //        } 
        //    //    } 
        //    //} 
        //    //if (throwOnError) 
        //    //{ 
        //    //    throw new ArgumentException();
        //    //}
        //    //return Type.GetType(name);


        //    //if (ht.ContainsKey(name))
        //    //    return (Type)ht[name];

        //    //Assembly winForms = Assembly.GetAssembly(typeof(Button));
        //    //Type[] types = winForms.GetTypes();
        //    //string typeName = String.Empty;
        //    //foreach (Type type in types)
        //    //{
        //    //    typeName = "system.windows.forms." + type.Name.ToLower();
        //    //    if (typeName == name.ToLower())
        //    //    {
        //    //        ht[name] = type;
        //    //        return type;
        //    //    }
        //    //}
        //    //return Type.GetType(name);
        //}

        //public void ReferenceAssembly(System.Reflection.AssemblyName name)
        //{
        //}








        private Hashtable assemblies = new Hashtable();

        public TypeResolutionService()
        {
        }

        /// We use this property to help us generate code and compile.
        public Assembly[] RefencedAssemblies
        {
            get
            {
                if (assemblies == null)
                {
                    assemblies = new Hashtable();
                }
                Assembly[] ret = new Assembly[assemblies.Values.Count];
                assemblies.Values.CopyTo(ret, 0);
                return ret;
            }
        }

        #region Implementation of ITypeResolutionService

        /// Add an assembly to our internal set.
        public void ReferenceAssembly(System.Reflection.AssemblyName name)
        {
            if (assemblies == null)
            {
                assemblies = new Hashtable();
            }

            if (!assemblies.Contains(name))
            {
                assemblies.Add(name, Assembly.Load(name));
            }
        }

        /// Search our internal set of assemblies for one with this AssemblyName.
        /// If it cannot be located and throwOnError is true, throw an exception.
        public System.Reflection.Assembly GetAssembly(System.Reflection.AssemblyName name, bool throwOnError)
        {
            Assembly a = null;

            if (assemblies != null)
            {
                a = assemblies[name] as Assembly;
            }

            if ((a == null) && throwOnError)
            {
                throw new Exception("Assembly " + name.Name + " not found in referenced assemblies.");
            }
            return a;
        }

        /// Search our internal set of assemblies for one with this AssemblyName.
        System.Reflection.Assembly System.ComponentModel.Design.ITypeResolutionService.GetAssembly(System.Reflection.AssemblyName name)
        {
            return GetAssembly(name, false);
        }

        /// Find a type based on a name that may or may not be fully qualified.
        /// If the type cannot be found and throwOnError is true, throw an Exception.
        /// Searching should ignore case based on ignoreCase's value.
        public Type GetType(string name, bool throwOnError, bool ignoreCase)
        {
            // First we assume it is a fully qualified name, and that Type.GetType()
            // can find it.

            Type t = Type.GetType(name, throwOnError, ignoreCase);


            if ((t == null))
            {
                AssemblyName[] assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
                foreach (AssemblyName an in assemblyNames)
                {
                    Assembly a = Assembly.Load(an);
                    Type[] types = a.GetTypes();
                    foreach (Type ty in types)
                    {
                        if (ty.FullName == name)
                        {
                            t = ty;
                            break;
                        }
                    }

                    if (t != null)
                        break;
                }
            }

            // No luck.
            if ((t == null) && throwOnError)
            {
                throw new Exception("The type " + name + " could not be found. " +
                    "If it is an unqualified name, then its assembly has not been referenced.");
            }

            return t;
        }

        /// Find a type based on a name that may or may not be fully qualified.
        /// If the type cannot be found and throwOnError is true, throw an Exception.
        /// Do not ignore case while searching.
        Type System.ComponentModel.Design.ITypeResolutionService.GetType(string name, bool throwOnError)
        {
            return GetType(name, throwOnError, false);
        }

        /// Find a type based on a name that may or may not be fully qualified.
        /// Do not ignore case while searching.
        Type System.ComponentModel.Design.ITypeResolutionService.GetType(string name)
        {
            return GetType(name, false, false);
        }

        /// Return the path to the file which the given assembly was loaded.
        /// If that assembly has not been loaded, this returns null.
        public string GetPathOfAssembly(System.Reflection.AssemblyName name)
        {
            Assembly a = GetAssembly(name, false);
            if (a != null)
            {
                return a.Location;
            }
            return null;
        }

        #endregion
	}// class
}// namespace
