﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microservices.BLL {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AppResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AppResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microservices.BLL.AppResource", typeof(AppResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to create user due to validation errors: {0}.
        /// </summary>
        public static string ERROR_CREATE_USER {
            get {
                return ResourceManager.GetString("ERROR_CREATE_USER", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Username or password is incorrect..
        /// </summary>
        public static string ERROR_INVALID_CREDENTIALS {
            get {
                return ResourceManager.GetString("ERROR_INVALID_CREDENTIALS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unexpected error has occured..
        /// </summary>
        public static string ERROR_UNKNOWN {
            get {
                return ResourceManager.GetString("ERROR_UNKNOWN", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User {0} already exists..
        /// </summary>
        public static string ERROR_USER_ALREADY_EXISTS {
            get {
                return ResourceManager.GetString("ERROR_USER_ALREADY_EXISTS", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user was not found..
        /// </summary>
        public static string ERROR_USER_NOT_FOUND {
            get {
                return ResourceManager.GetString("ERROR_USER_NOT_FOUND", resourceCulture);
            }
        }
    }
}
