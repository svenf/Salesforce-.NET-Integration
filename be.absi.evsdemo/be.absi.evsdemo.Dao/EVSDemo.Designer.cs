﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace be.absi.evsdemo.Dao
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class EVSDemoEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new EVSDemoEntities object using the connection string found in the 'EVSDemoEntities' section of the application configuration file.
        /// </summary>
        public EVSDemoEntities() : base("name=EVSDemoEntities", "EVSDemoEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EVSDemoEntities object.
        /// </summary>
        public EVSDemoEntities(string connectionString) : base(connectionString, "EVSDemoEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new EVSDemoEntities object.
        /// </summary>
        public EVSDemoEntities(EntityConnection connection) : base(connection, "EVSDemoEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<product> products
        {
            get
            {
                if ((_products == null))
                {
                    _products = base.CreateObjectSet<product>("products");
                }
                return _products;
            }
        }
        private ObjectSet<product> _products;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the products EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToproducts(product product)
        {
            base.AddObject("products", product);
        }

        

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="EVSDemoModel", Name="product")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class product : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new product object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        public static product Createproduct(global::System.Int32 id)
        {
            product product = new product();
            product.id = id;
            return product;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    OnidChanging(value);
                    ReportPropertyChanging("id");
                    _id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Int32 _id;
        partial void OnidChanging(global::System.Int32 value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String familyLevel1
        {
            get
            {
                return _familyLevel1;
            }
            set
            {
                OnfamilyLevel1Changing(value);
                ReportPropertyChanging("familyLevel1");
                _familyLevel1 = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("familyLevel1");
                OnfamilyLevel1Changed();
            }
        }
        private global::System.String _familyLevel1;
        partial void OnfamilyLevel1Changing(global::System.String value);
        partial void OnfamilyLevel1Changed();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String familyLevel2
        {
            get
            {
                return _familyLevel2;
            }
            set
            {
                OnfamilyLevel2Changing(value);
                ReportPropertyChanging("familyLevel2");
                _familyLevel2 = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("familyLevel2");
                OnfamilyLevel2Changed();
            }
        }
        private global::System.String _familyLevel2;
        partial void OnfamilyLevel2Changing(global::System.String value);
        partial void OnfamilyLevel2Changed();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String familyLevel3
        {
            get
            {
                return _familyLevel3;
            }
            set
            {
                OnfamilyLevel3Changing(value);
                ReportPropertyChanging("familyLevel3");
                _familyLevel3 = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("familyLevel3");
                OnfamilyLevel3Changed();
            }
        }
        private global::System.String _familyLevel3;
        partial void OnfamilyLevel3Changing(global::System.String value);
        partial void OnfamilyLevel3Changed();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String reference
        {
            get
            {
                return _reference;
            }
            set
            {
                OnreferenceChanging(value);
                ReportPropertyChanging("reference");
                _reference = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("reference");
                OnreferenceChanged();
            }
        }
        private global::System.String _reference;
        partial void OnreferenceChanging(global::System.String value);
        partial void OnreferenceChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String description
        {
            get
            {
                return _description;
            }
            set
            {
                OndescriptionChanging(value);
                ReportPropertyChanging("description");
                _description = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("description");
                OndescriptionChanged();
            }
        }
        private global::System.String _description;
        partial void OndescriptionChanging(global::System.String value);
        partial void OndescriptionChanged();

        #endregion

    
    }

    #endregion

    
}