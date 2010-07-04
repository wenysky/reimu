using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;

namespace NConvert.lybbs70_dnt31
{
    public partial class Provider : IProvider
    {
        string pkidname = "id";
        string tablename = "author";
        public int GetUsersRecordCount()
        {
            DBHelper userDBH = MainForm.GetSrcDBH_OldVer();
            return Convert.ToInt32(
                userDBH.ExecuteScalar(
                    string.Format("SELECT COUNT({1}) FROM {0}", MainForm.cic.SrcDbTablePrefix + tablename, pkidname)
                    )
                );
        }




        public List<Users> GetUserList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            //if (CurrentPage <= 1)
            //{
            //    sql = string.Format
            //           ("SELECT * FROM {0}user ORDER BY {1} LIMIT {1},{2}", MainForm.cic.SrcDbTablePrefix + tablename, MainForm.PageSize);
            //}
            //else
            //{
                sql = string.Format
                       ("SELECT * FROM {0} ORDER BY {3} LIMIT {1},{2}", MainForm.cic.SrcDbTablePrefix + tablename, MainForm.PageSize * (CurrentPage - 1), MainForm.PageSize, pkidname);
            //}
            #endregion

            DBHelper userlistDBH = MainForm.GetSrcDBH_OldVer();
            System.Data.Common.DbDataReader dr = userlistDBH.ExecuteReader(sql);
            List<Users> userlist = new List<Users>();
            while (dr.Read())
            {
                Users objUser = new Users();
                objUser.uid = Convert.ToInt32(dr["id"]);
                objUser.username = dr["username"].ToString();
#if DEBUG
                if (objUser.username.ToLower() == "admin")
                {
                    objUser.password = "e10adc3949ba59abbe56e057f20f883e";
                }
#else
                objUser.password = dr["password"] == DBNull.Value ? "" : dr["password"].ToString();
#endif
                if (dr["sex"] == DBNull.Value || dr["sex"].ToString() == "sect" || dr["sex"].ToString() == "")
                {
                    objUser.gender = 0;
                }
                else
                {
                    objUser.gender = dr["sex"].ToString() == "male" ? 1 : 2;
                }
                objUser.email = dr["email"] == DBNull.Value ? "" : dr["email"].ToString();
                objUser.groupid = GetNewUserGroupID(Convert.ToInt16(dr["usermode"]));
                //if (Convert.ToInt32(dr["LockUser"]) == 1 || Convert.ToInt32(dr["UserDel"]) == 1)
                //{
                //    objUser.groupid = 8;//未审核,未被激活,锁定,删除=>等待验证会员
                //}
                objUser.adminid = objUser.groupid < 4 ? objUser.groupid : 0;


                //objUser = dr["GroupExpirty"].ToString();
                objUser.extcredits1 = Convert.ToInt32(dr["money"]);
                objUser.extcredits2 = Convert.ToInt32(dr["experience"]);
                objUser.extcredits3 = Convert.ToInt32(dr["charm"]);
                objUser.extcredits4 = 0;// Convert.ToInt32(dr["Credit3"]);
                objUser.extcredits5 = 0;//Convert.ToInt32(dr["Credit4"]);
                objUser.extcredits6 = 0;//Convert.ToInt32(dr["Credit5"]);
                objUser.extcredits7 = 0;//Convert.ToInt32(dr["Credit6"]);
                objUser.lastip = dr["ipfrom"].ToString();
                objUser.lastactivity = Convert.ToDateTime(dr["lastactivetime"]);
                objUser.regip = dr["ipfrom"].ToString();
                objUser.joindate = Convert.ToDateTime(dr["registertime"]);
                objUser.oltime = 0;// Convert.ToInt32(dr["OnlineTotal"]);
                objUser.invisible = 0; //Convert.ToInt32(dr["Faded"]);//隐身
                objUser.posts = Convert.ToInt32(dr["nposts"]) + Convert.ToInt32(dr["nreply"]);
                objUser.digestposts = 0;// Convert.ToInt16(dr["Elites"]);


                if (dr["imageurl"] == DBNull.Value || dr["imageurl"].ToString().Trim() == "")
                {
                    objUser.avatar = @"avatars\common\0.gif";
                }
                else
                {
                    objUser.avatar = dr["imageurl"].ToString();//.Replace("0|", @"/");
                }
                //objUser.avatar = dr["signature"] == DBNull.Value ? objUser.avatar : "avatars\\upload\\" + dr["signature"].ToString();
                if (dr["imagewidth"] != DBNull.Value)
                {
                    objUser.avatarwidth = Convert.ToInt32(dr["imagewidth"]);
                }
                if (dr["imageheight"] != DBNull.Value)
                {
                    objUser.avatarheight = Convert.ToInt32(dr["imageheight"]);
                }
                if (dr["signature"] != DBNull.Value)
                {
                    objUser.signature = dr["signature"].ToString();
                    objUser.sightml = Utils.UBB.UBBToHTML(objUser.signature);
                }
                objUser.bio = dr["introdution"] == DBNull.Value ? "" : dr["introdution"].ToString();
                //objUser = dr[""].ToString();
                //objUser = dr[""].ToString();
                //objUser = dr[""].ToString();                
                //objUser = Convert.ToInt32(dr[""]);
                //objUser = Convert.ToInt32(dr[""]);
                //objUser = Convert.ToInt32(dr[""]);
                //objUser = Convert.ToInt32(dr[""]);
                userlist.Add(objUser);
            }
            return userlist;
        }

        private static short GetNewUserGroupID(short OldUserGroupID)
        {
            short NewGroupID;
            switch (OldUserGroupID)
            {
                case 7: NewGroupID = 1;
                    break;
                case 6: NewGroupID = 2;
                    break;
                case 3: NewGroupID = 3;
                    break;
                case 2: NewGroupID = 0;
#warning 需要完善 认证会员组
                    break;
                case 1:
                    NewGroupID = 0;
                    break;
                default:
                    throw new Exception("用户组错误");
            }

            if (NewGroupID > 5)//6以上的都转换到普通积分用户组中
            {
                NewGroupID = 10;
            }
            return NewGroupID;
        }
    }
}
