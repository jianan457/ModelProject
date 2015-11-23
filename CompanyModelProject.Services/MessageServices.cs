using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CompanyModelProject.Model;
using CompanyModelProject.IServices;
using CompanyModelProject.Common;
using CompanyModelProject.DataAccess;
namespace CompanyModelProject.Services
{
    public class MessageServices : IMessageServices
    {
        public static object obj = new object();

        private static IMessageServices instance = null;

        private MessageDataAccess access = new MessageDataAccess();

        public MessageServices() { }
        public static IMessageServices getinstance()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    instance = new MessageServices();
                }
            }

            return instance;
        }

        public int Insert(MessageModel model)
        {

            return access.Insert(model);
        }

        public int del_list(string where)
        {
            return access.del_list(where);
        }

        public MessageModel GetModel(int Id)
        {
            return access.GetOne(Id);
        }


        public List<MessageModel> Getlist()
        {
            return access.Getlist();
        }

        public int update(MessageModel model)
        {
            return access.update(model);
        }
        public List<MessageModel> GetPageList(int pageIndex, int pageSize, ref int allcount, ref int pagecount, string strwhere)
        {
            return access.GetPageList(pageIndex, pageSize, ref allcount, ref pagecount, strwhere);   
         }
    }
}
