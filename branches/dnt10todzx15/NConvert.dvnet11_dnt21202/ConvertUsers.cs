using System;
using System.Collections.Generic;
using System.Text;
using NConvert.Provider;
using NConvert.Entity;
using Yuwen.Tools.Data;

namespace NConvert.dvnet11_dnt21202
{
    public partial class Provider : IProvider
    {
        public int GetUsersRecordCount()
        {
            DBHelper userDBH = MainForm.GetSrcDBH_OldVer();
            return Convert.ToInt32(userDBH.ExecuteScalar(string.Format("SELECT COUNT(userid) FROM {0}user", MainForm.cic.SrcDbTablePrefix)));
        }




        public List<Users> GetUserList(int CurrentPage)
        {
            string sql;

            #region 分页语句
            if (CurrentPage <= 1)
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}user ORDER BY userid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize);
            }
            else
            {
                sql = string.Format
                       ("SELECT TOP {1} * FROM {0}user WHERE userid NOT IN (SELECT TOP {2} userid FROM {0}user ORDER BY userid) ORDER BY userid", MainForm.cic.SrcDbTablePrefix, MainForm.PageSize, MainForm.PageSize * (CurrentPage - 1));
            }
            #endregion

            DBHelper userlistDBH = MainForm.GetSrcDBH_OldVer();
            System.Data.Common.DbDataReader dr = userlistDBH.ExecuteReader(sql);
            List<Users> userlist = new List<Users>();
            while (dr.Read())
            {
                Users objUser = new Users();
                objUser.uid = Convert.ToInt32(dr["userid"]);
                objUser.username = dr["UserName"].ToString();
                objUser.password = dr["UserPassword"]==DBNull.Value? "" : dr["UserPassword"].ToString();
                objUser.gender = dr["UserSex"]==DBNull.Value? 0 : Convert.ToInt32(dr["UserSex"]) + 1;
                objUser.email = dr["UserEmail"] == DBNull.Value? "" : dr["UserEmail"].ToString();
                objUser.groupid = GetNewUserGroupID(Convert.ToInt16(dr["UserGroupID"]));
                if (Convert.ToInt32(dr["LockUser"]) == 1 || Convert.ToInt32(dr["UserDel"]) == 1)
                {
                    objUser.groupid = 8;//未审核,未被激活,锁定,删除=>等待验证会员
                }
                if (objUser.groupid == 1)
                {
                    objUser.adminid = 1;
                }


                //objUser = dr["GroupExpirty"].ToString();
                objUser.extcredits1 = Convert.ToInt32(dr["Credit0"]);
                objUser.extcredits2 = Convert.ToInt32(dr["Credit1"]);
                objUser.extcredits3 = Convert.ToInt32(dr["Credit2"]);
                objUser.extcredits4 = Convert.ToInt32(dr["Credit3"]);
                objUser.extcredits5 = Convert.ToInt32(dr["Credit4"]);
                objUser.extcredits6 = Convert.ToInt32(dr["Credit5"]);
                objUser.extcredits7 = Convert.ToInt32(dr["Credit6"]);
                objUser.lastip = dr["LastIp"].ToString();
                objUser.lastactivity = Convert.ToDateTime(dr["LastTime"]);
                objUser.regip = dr["JoinIp"].ToString();
                objUser.joindate = Convert.ToDateTime(dr["JoinTime"]);
                objUser.oltime = Convert.ToInt32(dr["OnlineTotal"]);
                objUser.invisible = Convert.ToInt32(dr["Faded"]);//隐身
                objUser.posts = Convert.ToInt32(dr["userposts"]);
                objUser.digestposts = Convert.ToInt16(dr["Elites"]);

                objUser.avatar = dr["Visualization"] == DBNull.Value ? objUser.avatar : "avatars\\upload\\" + dr["Visualization"].ToString();
                if (dr["VWidth"] != DBNull.Value)
                {
                    objUser.avatarwidth = Convert.ToInt32(dr["VWidth"]);
                }
                if (dr["VHeight"] != DBNull.Value)
                {
                    objUser.avatarheight = Convert.ToInt32(dr["VHeight"]);
                }
                if (dr["Signature"] != DBNull.Value)
                {
                    objUser.signature = dr["Signature"].ToString();
                    objUser.sightml = Utils.UBB.UBBToHTML(objUser.signature);
                }
                objUser.bio = dr["Info"] == DBNull.Value ? "" : dr["Info"].ToString();
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
                case 5: NewGroupID = 1;
                    break;
                case 3: NewGroupID = 3;
                    break;
                case 4: NewGroupID = 2;
                    break;
                case 2: NewGroupID = 10;//注册会员
                    break;
                default: NewGroupID = OldUserGroupID;
                    break;
            }

            if (NewGroupID > 5)//6以上的都转换到普通积分用户组中
            {
                NewGroupID = 10;
            }
            return NewGroupID;
        }
    }
}
