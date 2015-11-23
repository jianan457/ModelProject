using Controllers.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CompanyModelProject.Model;
namespace CompanyModelProject.Controllers
{
    public class BaseController : Controller
    {
        public AdminInfoModel  _model = null;
        public baseInfoModel _webBaseModel = null;
        public RightsOfUserModel _rModel = null;
        public BaseController()
        {
            _model=new  Common.UserManage().GetLoginUserInfo();
          List<baseInfoModel> list   = new CompanyModelProject.Services.baseInfoServices().Getlist();
          if (list!=null&&list.Count>0)
          {
              _webBaseModel = list[0];
          }
          List<RightsOfUserModel> rlist = new CompanyModelProject.Services.RightsOfUserServices().GetList().Where(o=>o.UserId==Id).ToList();
          if (rlist != null && rlist.Count > 0)
          {
              _rModel = rlist[0];
          }
        } 
        public string LoginName 
        {
            get { return _model == null ? string.Empty : _model.AdminName; }
        } 
        public int Id
        {
            get { return _model == null ? -1 : _model.ID; }
        }
        public string Email
        {
            get { return _model == null ? string.Empty : _model.Email; }
        }

        public string KeyWords
        {
            get { return _webBaseModel == null ? string.Empty : _webBaseModel.keywords; }
        }

        public string Description
        {
            get { return _webBaseModel == null ? string.Empty : _webBaseModel.description; }
        }

        public string Title
        {
            get { return _webBaseModel == null ? string.Empty : _webBaseModel.webName; }
        }

        public string RightsId
        {
            get { return _rModel == null ? string.Empty : _rModel.RightIds; }
        }
    }
}
