﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Config_PLC_SIEMENS
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="RtpConfig")]
	public partial class RtpConfigDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public RtpConfigDataContext() : 
				base(global::Config_PLC_SIEMENS.Properties.Settings.Default.RtpConfigConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public RtpConfigDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RtpConfigDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RtpConfigDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public RtpConfigDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetModule")]
		public ISingleResult<GetModuleResult> GetModule([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> rtpid)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), rtpid);
			return ((ISingleResult<GetModuleResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetRtpSignalGroups")]
		public ISingleResult<GetRtpSignalGroupsResult> GetRtpSignalGroups()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<GetRtpSignalGroupsResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetRtpSignals")]
		public ISingleResult<GetRtpSignalsResult> GetRtpSignals([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> groupid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> channeltype)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), groupid, channeltype);
			return ((ISingleResult<GetRtpSignalsResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetModulType")]
		public ISingleResult<GetModulTypeResult> GetModulType()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<GetModulTypeResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.AddNewModul")]
		public int AddNewModul([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> rtpid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> channelcount, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> modultype)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), rtpid, channelcount, modultype);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetSignalsIdForGroupId")]
		public ISingleResult<GetSignalsIdForGroupIdResult> GetSignalsIdForGroupId([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> signalgroupid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> channeltype)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), signalgroupid, channeltype);
			return ((ISingleResult<GetSignalsIdForGroupIdResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetChannel")]
		public ISingleResult<GetChannelResult> GetChannel([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> rtpid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> modulnumber)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), rtpid, modulnumber);
			return ((ISingleResult<GetChannelResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.GetChannelCurrentShbers")]
		public ISingleResult<GetChannelCurrentShbersResult> GetChannelCurrentShbers([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> rtpid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> chanelid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> groupid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> signalid)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), rtpid, chanelid, groupid, signalid);
			return ((ISingleResult<GetChannelCurrentShbersResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.CheckMountChannel")]
		public ISingleResult<CheckMountChannelResult> CheckMountChannel([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> rtpid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> modulnumber, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> channelnumber)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), rtpid, modulnumber, channelnumber);
			return ((ISingleResult<CheckMountChannelResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.ChangeModulType")]
		public int ChangeModulType([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> rtpid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> modulnumber, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> modultype)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), rtpid, modulnumber, modultype);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.ChangeCountChannel")]
		public int ChangeCountChannel([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> rtpid, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> modulnumber, [global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> channelcount)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), rtpid, modulnumber, channelcount);
			return ((int)(result.ReturnValue));
		}
	}
	
	public partial class GetModuleResult
	{
		
		private int _id;
		
		private int _modulnumber;
		
		private string _descript;
		
		public GetModuleResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL")]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_modulnumber", DbType="Int NOT NULL")]
		public int modulnumber
		{
			get
			{
				return this._modulnumber;
			}
			set
			{
				if ((this._modulnumber != value))
				{
					this._modulnumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_descript", DbType="NVarChar(2)")]
		public string descript
		{
			get
			{
				return this._descript;
			}
			set
			{
				if ((this._descript != value))
				{
					this._descript = value;
				}
			}
		}
	}
	
	public partial class GetRtpSignalGroupsResult
	{
		
		private int _id;
		
		private int _signalattrnumber;
		
		private int _signalgroup;
		
		private string _signalgroupdescription;
		
		public GetRtpSignalGroupsResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL")]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signalattrnumber", DbType="Int NOT NULL")]
		public int signalattrnumber
		{
			get
			{
				return this._signalattrnumber;
			}
			set
			{
				if ((this._signalattrnumber != value))
				{
					this._signalattrnumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signalgroup", DbType="Int NOT NULL")]
		public int signalgroup
		{
			get
			{
				return this._signalgroup;
			}
			set
			{
				if ((this._signalgroup != value))
				{
					this._signalgroup = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signalgroupdescription", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string signalgroupdescription
		{
			get
			{
				return this._signalgroupdescription;
			}
			set
			{
				if ((this._signalgroupdescription != value))
				{
					this._signalgroupdescription = value;
				}
			}
		}
	}
	
	public partial class GetRtpSignalsResult
	{
		
		private int _id;
		
		private string _signaldescription;
		
		private int _signalattribute;
		
		public GetRtpSignalsResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL")]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signaldescription", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string signaldescription
		{
			get
			{
				return this._signaldescription;
			}
			set
			{
				if ((this._signaldescription != value))
				{
					this._signaldescription = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signalattribute", DbType="Int NOT NULL")]
		public int signalattribute
		{
			get
			{
				return this._signalattribute;
			}
			set
			{
				if ((this._signalattribute != value))
				{
					this._signalattribute = value;
				}
			}
		}
	}
	
	public partial class GetModulTypeResult
	{
		
		private int _id;
		
		private string _descript;
		
		public GetModulTypeResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL")]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_descript", DbType="NVarChar(2) NOT NULL", CanBeNull=false)]
		public string descript
		{
			get
			{
				return this._descript;
			}
			set
			{
				if ((this._descript != value))
				{
					this._descript = value;
				}
			}
		}
	}
	
	public partial class GetSignalsIdForGroupIdResult
	{
		
		private int _id;
		
		private string _signaldescription;
		
		private int _signalattribute;
		
		public GetSignalsIdForGroupIdResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL")]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signaldescription", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string signaldescription
		{
			get
			{
				return this._signaldescription;
			}
			set
			{
				if ((this._signaldescription != value))
				{
					this._signaldescription = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signalattribute", DbType="Int NOT NULL")]
		public int signalattribute
		{
			get
			{
				return this._signalattribute;
			}
			set
			{
				if ((this._signalattribute != value))
				{
					this._signalattribute = value;
				}
			}
		}
	}
	
	public partial class GetChannelResult
	{
		
		private int _id;
		
		private int _channelnumber;
		
		private int _channeltype;
		
		private System.Nullable<int> _shiberid;
		
		private System.Nullable<int> _groupid;
		
		private System.Nullable<int> _signalid;
		
		private string _signalgroupdescription;
		
		private string _signaldescription;
		
		public GetChannelResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_id", DbType="Int NOT NULL")]
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_channelnumber", DbType="Int NOT NULL")]
		public int channelnumber
		{
			get
			{
				return this._channelnumber;
			}
			set
			{
				if ((this._channelnumber != value))
				{
					this._channelnumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_channeltype", DbType="Int NOT NULL")]
		public int channeltype
		{
			get
			{
				return this._channeltype;
			}
			set
			{
				if ((this._channeltype != value))
				{
					this._channeltype = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_shiberid", DbType="Int")]
		public System.Nullable<int> shiberid
		{
			get
			{
				return this._shiberid;
			}
			set
			{
				if ((this._shiberid != value))
				{
					this._shiberid = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_groupid", DbType="Int")]
		public System.Nullable<int> groupid
		{
			get
			{
				return this._groupid;
			}
			set
			{
				if ((this._groupid != value))
				{
					this._groupid = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signalid", DbType="Int")]
		public System.Nullable<int> signalid
		{
			get
			{
				return this._signalid;
			}
			set
			{
				if ((this._signalid != value))
				{
					this._signalid = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signalgroupdescription", DbType="NVarChar(50)")]
		public string signalgroupdescription
		{
			get
			{
				return this._signalgroupdescription;
			}
			set
			{
				if ((this._signalgroupdescription != value))
				{
					this._signalgroupdescription = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signaldescription", DbType="NVarChar(50)")]
		public string signaldescription
		{
			get
			{
				return this._signaldescription;
			}
			set
			{
				if ((this._signaldescription != value))
				{
					this._signaldescription = value;
				}
			}
		}
	}
	
	public partial class GetChannelCurrentShbersResult
	{
		
		private int _signaltype;
		
		private System.Nullable<int> _modulnumber;
		
		private System.Nullable<int> _channelnumber;
		
		private System.Nullable<int> _commandid;
		
		private int _signalcontrain;
		
		private System.Nullable<int> _shibernumber;
		
		public GetChannelCurrentShbersResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signaltype", DbType="Int NOT NULL")]
		public int signaltype
		{
			get
			{
				return this._signaltype;
			}
			set
			{
				if ((this._signaltype != value))
				{
					this._signaltype = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_modulnumber", DbType="Int")]
		public System.Nullable<int> modulnumber
		{
			get
			{
				return this._modulnumber;
			}
			set
			{
				if ((this._modulnumber != value))
				{
					this._modulnumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_channelnumber", DbType="Int")]
		public System.Nullable<int> channelnumber
		{
			get
			{
				return this._channelnumber;
			}
			set
			{
				if ((this._channelnumber != value))
				{
					this._channelnumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_commandid", DbType="Int")]
		public System.Nullable<int> commandid
		{
			get
			{
				return this._commandid;
			}
			set
			{
				if ((this._commandid != value))
				{
					this._commandid = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signalcontrain", DbType="Int NOT NULL")]
		public int signalcontrain
		{
			get
			{
				return this._signalcontrain;
			}
			set
			{
				if ((this._signalcontrain != value))
				{
					this._signalcontrain = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_shibernumber", DbType="Int")]
		public System.Nullable<int> shibernumber
		{
			get
			{
				return this._shibernumber;
			}
			set
			{
				if ((this._shibernumber != value))
				{
					this._shibernumber = value;
				}
			}
		}
	}
	
	public partial class CheckMountChannelResult
	{
		
		private int _signaltype;
		
		private System.Nullable<int> _modulnumber;
		
		private System.Nullable<int> _channelnumber;
		
		private System.Nullable<int> _commandid;
		
		private int _signalcontrain;
		
		private System.Nullable<int> _shibernumber;
		
		public CheckMountChannelResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signaltype", DbType="Int NOT NULL")]
		public int signaltype
		{
			get
			{
				return this._signaltype;
			}
			set
			{
				if ((this._signaltype != value))
				{
					this._signaltype = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_modulnumber", DbType="Int")]
		public System.Nullable<int> modulnumber
		{
			get
			{
				return this._modulnumber;
			}
			set
			{
				if ((this._modulnumber != value))
				{
					this._modulnumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_channelnumber", DbType="Int")]
		public System.Nullable<int> channelnumber
		{
			get
			{
				return this._channelnumber;
			}
			set
			{
				if ((this._channelnumber != value))
				{
					this._channelnumber = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_commandid", DbType="Int")]
		public System.Nullable<int> commandid
		{
			get
			{
				return this._commandid;
			}
			set
			{
				if ((this._commandid != value))
				{
					this._commandid = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_signalcontrain", DbType="Int NOT NULL")]
		public int signalcontrain
		{
			get
			{
				return this._signalcontrain;
			}
			set
			{
				if ((this._signalcontrain != value))
				{
					this._signalcontrain = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_shibernumber", DbType="Int")]
		public System.Nullable<int> shibernumber
		{
			get
			{
				return this._shibernumber;
			}
			set
			{
				if ((this._shibernumber != value))
				{
					this._shibernumber = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
