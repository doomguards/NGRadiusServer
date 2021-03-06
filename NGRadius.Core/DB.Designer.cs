﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
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
namespace NGRadius.Core
{
    #region 上下文
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    public partial class WinRadiusEntities : ObjectContext
    {
        #region 构造函数
    
        /// <summary>
        /// 请使用应用程序配置文件的“WinRadiusEntities”部分中的连接字符串初始化新 WinRadiusEntities 对象。
        /// </summary>
        public WinRadiusEntities() : base("name=WinRadiusEntities", "WinRadiusEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 WinRadiusEntities 对象。
        /// </summary>
        public WinRadiusEntities(string connectionString) : base(connectionString, "WinRadiusEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// 初始化新的 WinRadiusEntities 对象。
        /// </summary>
        public WinRadiusEntities(EntityConnection connection) : base(connection, "WinRadiusEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region 分部方法
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet 属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<Sys_Log> Sys_Log
        {
            get
            {
                if ((_Sys_Log == null))
                {
                    _Sys_Log = base.CreateObjectSet<Sys_Log>("Sys_Log");
                }
                return _Sys_Log;
            }
        }
        private ObjectSet<Sys_Log> _Sys_Log;
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        public ObjectSet<tbUsers> tbUsers
        {
            get
            {
                if ((_tbUsers == null))
                {
                    _tbUsers = base.CreateObjectSet<tbUsers>("tbUsers");
                }
                return _tbUsers;
            }
        }
        private ObjectSet<tbUsers> _tbUsers;

        #endregion

        #region AddTo 方法
    
        /// <summary>
        /// 用于向 Sys_Log EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddToSys_Log(Sys_Log sys_Log)
        {
            base.AddObject("Sys_Log", sys_Log);
        }
    
        /// <summary>
        /// 用于向 tbUsers EntitySet 添加新对象的方法，已弃用。请考虑改用关联的 ObjectSet&lt;T&gt; 属性的 .Add 方法。
        /// </summary>
        public void AddTotbUsers(tbUsers tbUsers)
        {
            base.AddObject("tbUsers", tbUsers);
        }

        #endregion

    }

    #endregion

    #region 实体
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="WinRadiusModel", Name="Sys_Log")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Sys_Log : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 Sys_Log 对象。
        /// </summary>
        /// <param name="recId">RecId 属性的初始值。</param>
        public static Sys_Log CreateSys_Log(global::System.Int64 recId)
        {
            Sys_Log sys_Log = new Sys_Log();
            sys_Log.RecId = recId;
            return sys_Log;
        }

        #endregion

        #region 基元属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 RecId
        {
            get
            {
                return _RecId;
            }
            set
            {
                if (_RecId != value)
                {
                    OnRecIdChanging(value);
                    ReportPropertyChanging("RecId");
                    _RecId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("RecId");
                    OnRecIdChanged();
                }
            }
        }
        private global::System.Int64 _RecId;
        partial void OnRecIdChanging(global::System.Int64 value);
        partial void OnRecIdChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> AddTime
        {
            get
            {
                return _AddTime;
            }
            set
            {
                OnAddTimeChanging(value);
                ReportPropertyChanging("AddTime");
                _AddTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("AddTime");
                OnAddTimeChanged();
            }
        }
        private Nullable<global::System.DateTime> _AddTime;
        partial void OnAddTimeChanging(Nullable<global::System.DateTime> value);
        partial void OnAddTimeChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Msg
        {
            get
            {
                return _Msg;
            }
            set
            {
                OnMsgChanging(value);
                ReportPropertyChanging("Msg");
                _Msg = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Msg");
                OnMsgChanged();
            }
        }
        private global::System.String _Msg;
        partial void OnMsgChanging(global::System.String value);
        partial void OnMsgChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String LogType
        {
            get
            {
                return _LogType;
            }
            set
            {
                OnLogTypeChanging(value);
                ReportPropertyChanging("LogType");
                _LogType = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("LogType");
                OnLogTypeChanged();
            }
        }
        private global::System.String _LogType;
        partial void OnLogTypeChanging(global::System.String value);
        partial void OnLogTypeChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CodeSoure
        {
            get
            {
                return _CodeSoure;
            }
            set
            {
                OnCodeSoureChanging(value);
                ReportPropertyChanging("CodeSoure");
                _CodeSoure = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CodeSoure");
                OnCodeSoureChanged();
            }
        }
        private global::System.String _CodeSoure;
        partial void OnCodeSoureChanging(global::System.String value);
        partial void OnCodeSoureChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Flag
        {
            get
            {
                return _Flag;
            }
            set
            {
                OnFlagChanging(value);
                ReportPropertyChanging("Flag");
                _Flag = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Flag");
                OnFlagChanged();
            }
        }
        private global::System.String _Flag;
        partial void OnFlagChanging(global::System.String value);
        partial void OnFlagChanged();

        #endregion

    
    }
    
    /// <summary>
    /// 没有元数据文档可用。
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="WinRadiusModel", Name="tbUsers")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class tbUsers : EntityObject
    {
        #region 工厂方法
    
        /// <summary>
        /// 创建新的 tbUsers 对象。
        /// </summary>
        /// <param name="recId">RecId 属性的初始值。</param>
        public static tbUsers CreatetbUsers(global::System.Int64 recId)
        {
            tbUsers tbUsers = new tbUsers();
            tbUsers.RecId = recId;
            return tbUsers;
        }

        #endregion

        #region 基元属性
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String username
        {
            get
            {
                return _username;
            }
            set
            {
                OnusernameChanging(value);
                ReportPropertyChanging("username");
                _username = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("username");
                OnusernameChanged();
            }
        }
        private global::System.String _username;
        partial void OnusernameChanging(global::System.String value);
        partial void OnusernameChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String password
        {
            get
            {
                return _password;
            }
            set
            {
                OnpasswordChanging(value);
                ReportPropertyChanging("password");
                _password = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("password");
                OnpasswordChanged();
            }
        }
        private global::System.String _password;
        partial void OnpasswordChanging(global::System.String value);
        partial void OnpasswordChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String groups
        {
            get
            {
                return _groups;
            }
            set
            {
                OngroupsChanging(value);
                ReportPropertyChanging("groups");
                _groups = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("groups");
                OngroupsChanged();
            }
        }
        private global::System.String _groups;
        partial void OngroupsChanging(global::System.String value);
        partial void OngroupsChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String addr
        {
            get
            {
                return _addr;
            }
            set
            {
                OnaddrChanging(value);
                ReportPropertyChanging("addr");
                _addr = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("addr");
                OnaddrChanged();
            }
        }
        private global::System.String _addr;
        partial void OnaddrChanging(global::System.String value);
        partial void OnaddrChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Double> cash
        {
            get
            {
                return _cash;
            }
            set
            {
                OncashChanging(value);
                ReportPropertyChanging("cash");
                _cash = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("cash");
                OncashChanged();
            }
        }
        private Nullable<global::System.Double> _cash;
        partial void OncashChanging(Nullable<global::System.Double> value);
        partial void OncashChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String expiry
        {
            get
            {
                return _expiry;
            }
            set
            {
                OnexpiryChanging(value);
                ReportPropertyChanging("expiry");
                _expiry = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("expiry");
                OnexpiryChanged();
            }
        }
        private global::System.String _expiry;
        partial void OnexpiryChanging(global::System.String value);
        partial void OnexpiryChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String others
        {
            get
            {
                return _others;
            }
            set
            {
                OnothersChanging(value);
                ReportPropertyChanging("others");
                _others = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("others");
                OnothersChanged();
            }
        }
        private global::System.String _others;
        partial void OnothersChanging(global::System.String value);
        partial void OnothersChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String method
        {
            get
            {
                return _method;
            }
            set
            {
                OnmethodChanging(value);
                ReportPropertyChanging("method");
                _method = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("method");
                OnmethodChanged();
            }
        }
        private global::System.String _method;
        partial void OnmethodChanging(global::System.String value);
        partial void OnmethodChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String billtype
        {
            get
            {
                return _billtype;
            }
            set
            {
                OnbilltypeChanging(value);
                ReportPropertyChanging("billtype");
                _billtype = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("billtype");
                OnbilltypeChanged();
            }
        }
        private global::System.String _billtype;
        partial void OnbilltypeChanging(global::System.String value);
        partial void OnbilltypeChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String EmpNo
        {
            get
            {
                return _EmpNo;
            }
            set
            {
                OnEmpNoChanging(value);
                ReportPropertyChanging("EmpNo");
                _EmpNo = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("EmpNo");
                OnEmpNoChanged();
            }
        }
        private global::System.String _EmpNo;
        partial void OnEmpNoChanging(global::System.String value);
        partial void OnEmpNoChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String EmpName
        {
            get
            {
                return _EmpName;
            }
            set
            {
                OnEmpNameChanging(value);
                ReportPropertyChanging("EmpName");
                _EmpName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("EmpName");
                OnEmpNameChanged();
            }
        }
        private global::System.String _EmpName;
        partial void OnEmpNameChanging(global::System.String value);
        partial void OnEmpNameChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Detp
        {
            get
            {
                return _Detp;
            }
            set
            {
                OnDetpChanging(value);
                ReportPropertyChanging("Detp");
                _Detp = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Detp");
                OnDetpChanged();
            }
        }
        private global::System.String _Detp;
        partial void OnDetpChanging(global::System.String value);
        partial void OnDetpChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 RecId
        {
            get
            {
                return _RecId;
            }
            set
            {
                if (_RecId != value)
                {
                    OnRecIdChanging(value);
                    ReportPropertyChanging("RecId");
                    _RecId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("RecId");
                    OnRecIdChanged();
                }
            }
        }
        private global::System.Int64 _RecId;
        partial void OnRecIdChanging(global::System.Int64 value);
        partial void OnRecIdChanged();
    
        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String EQType
        {
            get
            {
                return _EQType;
            }
            set
            {
                OnEQTypeChanging(value);
                ReportPropertyChanging("EQType");
                _EQType = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("EQType");
                OnEQTypeChanged();
            }
        }
        private global::System.String _EQType;
        partial void OnEQTypeChanging(global::System.String value);
        partial void OnEQTypeChanged();

        #endregion

    
    }

    #endregion

    
}
