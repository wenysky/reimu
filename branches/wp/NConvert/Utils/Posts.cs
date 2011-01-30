using System;
using System.Collections.Generic;
using System.Text;
using Yuwen.Tools.TinyData;
using NConvert.Entity;
using System.Text.RegularExpressions;

namespace NConvert.Utils
{
    public class Posts
    {
        ///<summary>
        /// 建立分表 (请先取消掉dnt_tablelist表的自增)
        /// </summary>
        /// <param name="PostsTableId">分表id</param>
        /// <param name="mintid">最小tid</param>
        /// <param name="maxtid">最大tid</param>       
        public static void CreatePostsTable(int PostsTableId, int mintid, int maxtid)
        {
            //更新上一个分表信息
            //DBHelper UpdateTableListDBH = MainForm.GetTargetDBH();
            //UpdateTableListDBH.ExecuteNonQuery(string.Format("UPDATE {0}tablelist SET maxtid={1} WHERE id={2}", MainForm.cic.TargetDbTablePrefix, TopicId, PostsTableId - 1));
            //UpdateTableListDBH.Dispose();


            #region 建表

            string tablename = MainForm.cic.TargetDbTablePrefix + "posts" + PostsTableId;
            StringBuilder sbTable = new StringBuilder();
            sbTable.Append("IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[" + tablename + "]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)  DROP TABLE [" + tablename + "];");
            sbTable.Append("CREATE TABLE [" + tablename + "] ([pid] [int] NOT NULL ,[fid] [int] NOT NULL ," +
                "[tid] [int] NOT NULL ,[parentid] [int] NOT NULL ,[layer] [int] NOT NULL ,[poster] [nvarchar] (20) NOT NULL ," +
                "[posterid] [int] NOT NULL ,[title] [nvarchar] (80) NOT NULL ,[postdatetime] [smalldatetime] NOT NULL ," +
                "[message] [ntext] NOT NULL ,[ip] [nvarchar] (15) NOT NULL ," +
                "[lastedit] [nvarchar] (50) NOT NULL ,[invisible] [int] NOT NULL ,[usesig] [int] NOT NULL ,[htmlon] [int] NOT NULL ," +
                "[smileyoff] [int] NOT NULL ,[parseurloff] [int] NOT NULL ,[bbcodeoff] [int] NOT NULL ,[attachment] [int] NOT NULL ,[rate] [int] NOT NULL ," +
                "[ratetimes] [int] NOT NULL ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");
            sbTable.Append(";");
            sbTable.Append("ALTER TABLE [" + tablename + "] WITH NOCHECK ADD CONSTRAINT [PK_" + tablename + "] PRIMARY KEY  CLUSTERED ([pid])  ON [PRIMARY]");
            sbTable.Append(";");

            sbTable.Append("ALTER TABLE [" + tablename + "] ADD ");
            sbTable.Append("CONSTRAINT [DF_" + tablename + "_pid] DEFAULT (0) FOR [pid],");
            sbTable.Append("CONSTRAINT [DF_" + tablename + "_parentid] DEFAULT (0) FOR [parentid],CONSTRAINT [DF_" + tablename + "_layer] DEFAULT (0) FOR [layer],");
            sbTable.Append("CONSTRAINT [DF_" + tablename + "_poster] DEFAULT ('') FOR [poster],CONSTRAINT [DF_" + tablename + "_posterid] DEFAULT (0) FOR [posterid],");
            sbTable.Append("CONSTRAINT [DF_" + tablename + "_postdatetime] DEFAULT (getdate()) FOR [postdatetime],CONSTRAINT [DF_" + tablename + "_message] DEFAULT ('') FOR [message],");
            sbTable.Append("CONSTRAINT [DF_" + tablename + "_ip] DEFAULT ('') FOR [ip],CONSTRAINT [DF_" + tablename + "_lastedit] DEFAULT ('') FOR [lastedit],");
            sbTable.Append("CONSTRAINT [DF_" + tablename + "_invisible] DEFAULT (0) FOR [invisible],CONSTRAINT [DF_" + tablename + "_usesig] DEFAULT (0) FOR [usesig],");
            sbTable.Append("CONSTRAINT [DF_" + tablename + "_htmlon] DEFAULT (0) FOR [htmlon],CONSTRAINT [DF_" + tablename + "_smileyoff] DEFAULT (0) FOR [smileyoff],");
            sbTable.Append("CONSTRAINT [DF_" + tablename + "_parseurloff] DEFAULT (0) FOR [parseurloff],CONSTRAINT [DF_" + tablename + "_bbcodeoff] DEFAULT (0) FOR [bbcodeoff],");
            sbTable.Append("CONSTRAINT [DF_" + tablename + "_attachment] DEFAULT (0) FOR [attachment],CONSTRAINT [DF_" + tablename + "_rate] DEFAULT (0) FOR [rate],");
            sbTable.Append("CONSTRAINT [DF_" + tablename + "_ratetimes] DEFAULT (0) FOR [ratetimes]");

            sbTable.Append(";");
            sbTable.Append("CREATE  INDEX [parentid] ON [" + tablename + "]([parentid]) ON [PRIMARY]");
            sbTable.Append(";");

            sbTable.Append("CREATE  UNIQUE  INDEX [showtopic] ON [" + tablename + "]([tid], [invisible], [pid]) ON [PRIMARY]");
            sbTable.Append(";");


            sbTable.Append("CREATE  INDEX [treelist] ON [" + tablename + "]([tid], [invisible], [parentid]) ON [PRIMARY]");
            sbTable.Append(";");

            #endregion

            //执行建表语句
            DBHelper CreateNewTableDBH = MainForm.GetTargetDBH();
            CreateNewTableDBH.Open();
            CreateNewTableDBH.ExecuteNonQuery(sbTable.ToString());
            CreateNewTableDBH.Dispose();

            #region 创建分表存储过程
//2.1.202
//            StringBuilder sbProc = new StringBuilder(@"
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_createpost]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_createpost]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_getfirstpostid]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_getfirstpostid]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_getpostcount]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_getpostcount]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_deletepostbypid]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_deletepostbypid]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_getposttree]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_getposttree]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_getsinglepost]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_getsinglepost]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_updatepost]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_updatepost]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_getnewtopics]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_getnewtopics]
//
//                    ~
//
//                    CREATE PROCEDURE dnt_createpost
//                    @fid int,
//                    @tid int,
//                    @parentid int,
//                    @layer int,
//                    @poster varchar(20),
//                    @posterid int,
//                    @title nvarchar(60),
//                    @postdatetime char(20),
//                    @message ntext,
//                    @ip varchar(15),
//                    @lastedit varchar(50),
//                    @invisible int,
//                    @usesig int,
//                    @htmlon int,
//                    @smileyoff int,
//                    @bbcodeoff int,
//                    @parseurloff int,
//                    @attachment int,
//                    @rate int,
//                    @ratetimes int
//
//                    AS
//
//
//                    DEClARE @postid int
//
//                    DELETE FROM [dnt_postid] WHERE DATEDIFF(n, postdatetime, GETDATE()) >5
//
//                    INSERT INTO [dnt_postid] ([postdatetime]) VALUES(GETDATE())
//
//                    SELECT @postid=SCOPE_IDENTITY()
//
//                    INSERT INTO [dnt_posts1]([pid], [fid], [tid], [parentid], [layer], [poster], [posterid], [title], [postdatetime], [message], [ip], [lastedit], [invisible], [usesig], [htmlon], [smileyoff], [bbcodeoff], [parseurloff], [attachment], [rate], [ratetimes]) VALUES(@postid, @fid, @tid, @parentid, @layer, @poster, @posterid, @title, @postdatetime, @message, @ip, @lastedit, @invisible, @usesig, @htmlon, @smileyoff, @bbcodeoff, @parseurloff, @attachment, @rate, @ratetimes)
//
//                    IF @parentid=0
//                        BEGIN
//                    		
//                            UPDATE [dnt_posts1] SET [parentid]=@postid WHERE [pid]=@postid
//                        END
//
//                    IF @@ERROR=0
//                        BEGIN
//                            IF  @invisible = 0
//                            BEGIN
//                    		
//                                UPDATE [dnt_statistics] SET [totalpost]=[totalpost] + 1
//                    		
//                    		
//                    		
//                                DECLARE @fidlist AS VARCHAR(1000)
//                                DECLARE @strsql AS VARCHAR(4000)
//                    			
//                                SET @fidlist = '';
//                    			
//                                SELECT @fidlist = ISNULL([parentidlist],'') FROM [dnt_forums] WHERE [fid] = @fid
//                                IF RTRIM(@fidlist)<>''
//	                                BEGIN
//		                                SET @fidlist = RTRIM(@fidlist) + ',' + CAST(@fid AS VARCHAR(10))
//	                                END
//                                ELSE
//	                                BEGIN
//		                                SET @fidlist = CAST(@fid AS VARCHAR(10))
//	                                END
//                            
//                    			
//                                UPDATE [dnt_forums] SET 
//						                                [posts]=[posts] + 1, 
//						                                [todayposts]=CASE 
//										                                WHEN DATEDIFF(day, [lastpost], GETDATE())=0 THEN [todayposts]*1 + 1 
//									                                 ELSE 1 
//									                                 END,
//						                                [lasttid]=@tid,	
//						                                [lasttitle]=@title,
//						                                [lastpost]=@postdatetime,
//						                                [lastposter]=@poster,
//						                                [lastposterid]=@posterid 
//                    							
//				                                WHERE (CHARINDEX(',' + RTRIM([fid]) + ',', ',' + (SELECT @fidlist AS [fid]) + ',') > 0)
//                    			
//                    			
//                                UPDATE [dnt_users] SET
//	                                [lastpost] = @postdatetime,
//	                                [lastpostid] = @postid,
//	                                [lastposttitle] = @title,
//	                                [posts] = [posts] + 1,
//	                                [lastactivity] = GETDATE()
//                                WHERE [uid] = @posterid
//                            
//                            
//                                IF @layer<=0
//	                                BEGIN
//		                                UPDATE [dnt_topics] SET [replies]=0,[lastposter]=@poster,[lastpost]=@postdatetime,[lastposterid]=@posterid WHERE [tid]=@tid
//	                                END
//                                ELSE
//	                                BEGIN
//		                                UPDATE [dnt_topics] SET [replies]=[replies] + 1,[lastposter]=@poster,[lastpost]=@postdatetime,[lastposterid]=@posterid WHERE [tid]=@tid
//	                                END
//                            END
//
//                            UPDATE [dnt_topics] SET [lastpostid]=@postid WHERE [tid]=@tid
//                            
//                        IF @posterid <> -1
//                            BEGIN
//                                INSERT [dnt_myposts]([uid], [tid], [pid], [dateline]) VALUES(@posterid, @tid, @postid, @postdatetime)
//                            END
//
//                        END
//                    	
//                    SELECT @postid AS postid
//
//                    ~
//
//
//                    CREATE PROCEDURE dnt_getfirstpostid
//                    @tid int
//                    AS
//                    SELECT TOP 1 [pid] FROM [dnt_posts1] WHERE [tid]=@tid ORDER BY [pid]
//
//                    ~
//
//
//                    CREATE PROCEDURE dnt_getpostcount
//                    @tid int
//                    AS
//                    SELECT COUNT(pid) FROM [dnt_posts1] WHERE [tid]=@tid AND [invisible]=0 AND layer>0
//
//                    ~
//
//
//                    CREATE  PROCEDURE dnt_deletepostbypid
//                        @pid int
//                    AS
//
//                        DECLARE @fid int
//                        DECLARE @tid int
//                        DECLARE @posterid int
//                        DECLARE @lastforumposterid int
//                        DECLARE @layer int
//                        DECLARE @postdatetime smalldatetime
//                        DECLARE @poster varchar(50)
//                        DECLARE @postcount int
//                        DECLARE @title nchar(60)
//                        DECLARE @lasttid int
//                        DECLARE @postid int
//                        DECLARE @todaycount int
//                    	
//                    	
//                        SELECT @fid = [fid],@tid = [tid],@posterid = [posterid],@layer = [layer], @postdatetime = [postdatetime] FROM [dnt_posts1] WHERE pid = @pid
//
//                        DECLARE @fidlist AS VARCHAR(1000)
//                    	
//                        SET @fidlist = '';
//                    	
//                        SELECT @fidlist = ISNULL([parentidlist],'') FROM [dnt_forums] WHERE [fid] = @fid
//                        IF RTRIM(@fidlist)<>''
//                            BEGIN
//                                SET @fidlist = RTRIM(@fidlist) + ',' + CAST(@fid AS VARCHAR(10))
//                            END
//                        ELSE
//                            BEGIN
//                                SET @fidlist = CAST(@fid AS VARCHAR(10))
//                            END
//
//
//                        IF @layer<>0
//
//                            BEGIN
//                    			
//                                UPDATE [dnt_statistics] SET [totalpost]=[totalpost] - 1
//
//                                UPDATE [dnt_forums] SET 
//	                                [posts]=[posts] - 1, 
//	                                [todayposts]=CASE 
//						                                WHEN DATEPART(yyyy, @postdatetime)=DATEPART(yyyy,GETDATE()) AND DATEPART(mm, @postdatetime)=DATEPART(mm,GETDATE()) AND DATEPART(dd, @postdatetime)=DATEPART(dd,GETDATE()) THEN [todayposts] - 1
//						                                ELSE [todayposts]
//				                                END						
//                                WHERE (CHARINDEX(',' + RTRIM([fid]) + ',', ',' +
//					                                (SELECT @fidlist AS [fid]) + ',') > 0)
//                    			
//                                UPDATE [dnt_users] SET			
//	                                [posts] = [posts] - 1
//                                WHERE [uid] = @posterid
//
//                                UPDATE [dnt_topics] SET [replies]=[replies] - 1 WHERE [tid]=@tid
//                    			
//                                DELETE FROM [dnt_posts1] WHERE [pid]=@pid
//                    			
//                            END
//                        ELSE
//                            BEGIN
//                    		
//                                SELECT @postcount = COUNT([pid]) FROM [dnt_posts1] WHERE [tid] = @tid
//                                SELECT @todaycount = COUNT([pid]) FROM [dnt_posts1] WHERE [tid] = @tid AND DATEDIFF(d, [postdatetime], GETDATE()) = 0
//                    			
//
//                                UPDATE [dnt_statistics] SET [totaltopic]=[totaltopic] - 1, [totalpost]=[totalpost] - @postcount
//                    			
//                                UPDATE [dnt_forums] SET [posts]=[posts] - @postcount, [topics]=[topics] - 1,[todayposts]=[todayposts] - @todaycount WHERE (CHARINDEX(',' + RTRIM([fid]) + ',', ',' +(SELECT @fidlist AS [fid]) + ',') > 0)
//                    			
//                                UPDATE [dnt_users] SET
//	                                [posts] = [posts] - @postcount					
//                                WHERE [uid] = @posterid
//                    			
//
//                                DELETE FROM [dnt_posts1] WHERE [tid] = @tid
//                    			
//                                DELETE FROM [dnt_topics] WHERE [tid] = @tid
//                    			
//                            END	
//                    		
//
//                        IF @layer<>0
//                            BEGIN
//                                SELECT TOP 1 @pid = [pid], @posterid = [posterid], @postdatetime = [postdatetime], @title = [title], @poster = [poster] FROM [dnt_posts1] WHERE [tid]=@tid ORDER BY [pid] DESC
//                                UPDATE [dnt_topics] SET [lastposter]=@poster,[lastpost]=@postdatetime,[lastpostid]=@pid,[lastposterid]=@posterid WHERE [tid]=@tid
//                            END
//
//
//
//                        SELECT @lasttid = [lasttid] FROM [dnt_forums] WHERE [fid] = @fid
//
//                    	
//                        IF @lasttid = @tid
//                            BEGIN
//
//                    			
//                    			
//
//                                SELECT TOP 1 @pid = [pid], @tid = [tid],@lastforumposterid = [posterid], @title = [title], @postdatetime = [postdatetime], @poster = [poster] FROM [dnt_posts1] WHERE [fid] = @fid ORDER BY [pid] DESC
//                    			
//                            
//                            
//                                UPDATE [dnt_forums] SET 
//                    			
//	                                [lasttid]=@tid,
//	                                [lasttitle]=ISNULL(@title,''),
//	                                [lastpost]=@postdatetime,
//	                                [lastposter]=ISNULL(@poster,''),
//	                                [lastposterid]=ISNULL(@lastforumposterid,'0')
//
//                                WHERE (CHARINDEX(',' + RTRIM([fid]) + ',', ',' +
//					                                (SELECT @fidlist AS [fid]) + ',') > 0)
//
//
//                    			
//                                SELECT TOP 1 @pid = [pid], @tid = [tid],@posterid = [posterid], @postdatetime = [postdatetime], @title = [title], @poster = [poster] FROM [dnt_posts1] WHERE [posterid]=@posterid ORDER BY [pid] DESC
//                    			
//                                UPDATE [dnt_users] SET
//                    			
//	                                [lastpost] = @postdatetime,
//	                                [lastpostid] = @pid,
//	                                [lastposttitle] = ISNULL(@title,'')
//                    				
//                                WHERE [uid] = @posterid
//                    			
//                            END
//
//
//                    ~
//
//
//                    CREATE PROCEDURE dnt_getposttree
//                    @tid int
//                    AS
//                    SELECT [pid], [layer], [title], [poster], [posterid],[postdatetime],[message] FROM [dnt_posts1] WHERE [tid]=@tid AND [invisible]=0 ORDER BY [parentid];
//
//
//                    ~
//
//                    CREATE PROCEDURE dnt_getsinglepost
//                    @tid int,
//                    @pid int
//                    AS
//                    SELECT [aid], [tid], [pid], [postdatetime], [readperm], [filename], [description], [filetype], [filesize], [attachment], [downloads] FROM [dnt_attachments] WHERE [tid]=@tid
//
//                    SELECT TOP 1 
//	                                [dnt_posts1].[pid], 
//	                                [dnt_posts1].[fid], 
//	                                [dnt_posts1].[title], 
//	                                [dnt_posts1].[layer],
//	                                [dnt_posts1].[message], 
//	                                [dnt_posts1].[ip], 
//	                                [dnt_posts1].[lastedit], 
//	                                [dnt_posts1].[postdatetime], 
//	                                [dnt_posts1].[attachment], 
//	                                [dnt_posts1].[poster], 
//	                                [dnt_posts1].[invisible], 
//	                                [dnt_posts1].[usesig], 
//	                                [dnt_posts1].[htmlon], 
//	                                [dnt_posts1].[smileyoff], 
//	                                [dnt_posts1].[parseurloff], 
//	                                [dnt_posts1].[bbcodeoff], 
//	                                [dnt_posts1].[rate], 
//	                                [dnt_posts1].[ratetimes], 
//	                                [dnt_posts1].[posterid], 
//	                                [dnt_users].[nickname],  
//	                                [dnt_users].[username], 
//	                                [dnt_users].[groupid],
//                                    [dnt_users].[spaceid],
//                                    [dnt_users].[gender],
//									[dnt_users].[bday], 
//	                                [dnt_users].[email], 
//	                                [dnt_users].[showemail], 
//	                                [dnt_users].[digestposts], 
//	                                [dnt_users].[credits], 
//	                                [dnt_users].[extcredits1], 
//	                                [dnt_users].[extcredits2], 
//	                                [dnt_users].[extcredits3], 
//	                                [dnt_users].[extcredits4], 
//	                                [dnt_users].[extcredits5], 
//	                                [dnt_users].[extcredits6], 
//	                                [dnt_users].[extcredits7], 
//	                                [dnt_users].[extcredits8], 
//	                                [dnt_users].[posts], 
//	                                [dnt_users].[joindate], 
//	                                [dnt_users].[onlinestate], 
//	                                [dnt_users].[lastactivity], 
//	                                [dnt_users].[invisible], 
//	                                [dnt_userfields].[avatar], 
//	                                [dnt_userfields].[avatarwidth], 
//	                                [dnt_userfields].[avatarheight], 
//	                                [dnt_userfields].[medals], 
//	                                [dnt_userfields].[sightml] AS signature, 
//	                                [dnt_userfields].[location], 
//	                                [dnt_userfields].[customstatus], 
//	                                [dnt_userfields].[website], 
//	                                [dnt_userfields].[icq], 
//	                                [dnt_userfields].[qq], 
//	                                [dnt_userfields].[msn], 
//	                                [dnt_userfields].[yahoo], 
//	                                [dnt_userfields].[skype] 
//                    FROM [dnt_posts1] LEFT JOIN [dnt_users] ON [dnt_users].[uid]=[dnt_posts1].[posterid] LEFT JOIN [dnt_userfields] ON [dnt_userfields].[uid]=[dnt_users].[uid] WHERE [dnt_posts1].[pid]=@pid
//
//
//                    ~
//
//                    CREATE PROCEDURE dnt_updatepost
//                        @pid int,
//                        @title nvarchar(160),
//                        @message ntext,
//                        @lastedit nvarchar(50),
//                        @invisible int,
//                        @usesig int,
//                        @htmlon int,
//                        @smileyoff int,
//                        @bbcodeoff int,
//                        @parseurloff int
//                    AS
//                    UPDATE dnt_posts1 SET 
//                        [title]=@title,
//                        [message]=@message,
//                        [lastedit]=@lastedit,
//                        [invisible]=@invisible,
//                        [usesig]=@usesig,
//                        [htmlon]=@htmlon,
//                        [smileyoff]=@smileyoff,
//                        [bbcodeoff]=@bbcodeoff,
//                        [parseurloff]=@parseurloff WHERE [pid]=@pid
//
//
//                    ~
//
//                    CREATE PROCEDURE dnt_getnewtopics 
//                    @fidlist VARCHAR(500)
//                    AS
//                    IF @fidlist<>''
//                    BEGIN
//                    DECLARE @strSQL VARCHAR(5000)
//                    SET @strSQL = 'SELECT TOP 20   [dnt_posts1].[tid], [dnt_posts1].[title], [dnt_posts1].[poster], [dnt_posts1].[postdatetime], [dnt_posts1].[message],[dnt_forums].[name] FROM [dnt_posts1]  LEFT JOIN [dnt_forums] ON [dnt_posts1].[fid]=[dnt_forums].[fid] WHERE  [dnt_forums].[fid] NOT IN ('+@fidlist +')  AND [dnt_posts1].[layer]=0 ORDER BY [dnt_posts1].[pid] DESC' 
//                    END
//                    ELSE
//                    BEGIN
//                    SET @strSQL = 'SELECT TOP 20   [dnt_posts1].[tid], [dnt_posts1].[title], [dnt_posts1].[poster], [dnt_posts1].[postdatetime], [dnt_posts1].[message],[dnt_forums].[name] FROM [dnt_posts1]  LEFT JOIN [dnt_forums] ON [dnt_posts1].[fid]=[dnt_forums].[fid] WHERE [dnt_posts1].[layer]=0 ORDER BY [dnt_posts1].[pid] DESC'
//                    END
//                    EXEC(@strSQL)
//
//               ");

//            sbProc.Replace("\"", "'").Replace("dnt_posts1", MainForm.cic.TargetDbTablePrefix + "posts" + PostsTableId);
//            sbProc.Replace("maxtablelistid", PostsTableId.ToString());
//            sbProc.Replace("dnt_createpost", MainForm.cic.TargetDbTablePrefix + "createpost" + PostsTableId);
//            sbProc.Replace("dnt_getfirstpostid", MainForm.cic.TargetDbTablePrefix + "getfirstpost" + PostsTableId + "id");
//            sbProc.Replace("dnt_getpostcount", MainForm.cic.TargetDbTablePrefix + "getpost" + PostsTableId + "count");
//            sbProc.Replace("dnt_deletepostbypid", MainForm.cic.TargetDbTablePrefix + "deletepost" + PostsTableId + "bypid");
//            sbProc.Replace("dnt_getposttree", MainForm.cic.TargetDbTablePrefix + "getpost" + PostsTableId + "tree");
//            sbProc.Replace("dnt_getsinglepost", MainForm.cic.TargetDbTablePrefix + "getsinglepost" + PostsTableId);
//            sbProc.Replace("dnt_updatepost", MainForm.cic.TargetDbTablePrefix + "updatepost" + PostsTableId);
//            sbProc.Replace("dnt_getnewtopics", MainForm.cic.TargetDbTablePrefix + "getnewtopics");
//            sbProc.Replace("dnt_", MainForm.cic.TargetDbTablePrefix);
            StringBuilder sbProc = new StringBuilder(@"

                    if exists (select * from sysobjects where id = object_id(N'[dnt_createpost]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
                    drop procedure [dnt_createpost]

                    ~

                    if exists (select * from sysobjects where id = object_id(N'[dnt_getfirstpostid]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
                    drop procedure [dnt_getfirstpostid]

                    ~

                    if exists (select * from sysobjects where id = object_id(N'[dnt_getpostcount]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
                    drop procedure [dnt_getpostcount]

                    ~

                    if exists (select * from sysobjects where id = object_id(N'[dnt_deletepostbypid]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
                    drop procedure [dnt_deletepostbypid]

                    ~

                    if exists (select * from sysobjects where id = object_id(N'[dnt_getposttree]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
                    drop procedure [dnt_getposttree]

                    ~

                    if exists (select * from sysobjects where id = object_id(N'[dnt_getsinglepost]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
                    drop procedure [dnt_getsinglepost]

                    ~

                    if exists (select * from sysobjects where id = object_id(N'[dnt_updatepost]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
                    drop procedure [dnt_updatepost]

                    ~

                    if exists (select * from sysobjects where id = object_id(N'[dnt_getnewtopics]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
                    drop procedure [dnt_getnewtopics]

                    ~

                    CREATE PROCEDURE dnt_createpost
                    @fid int,
                    @tid int,
                    @parentid int,
                    @layer int,
                    @poster varchar(20),
                    @posterid int,
                    @title nvarchar(60),
                    @topictitle nvarchar(60),
                    @postdatetime char(20),
                    @message ntext,
                    @ip varchar(15),
                    @lastedit varchar(50),
                    @invisible int,
                    @usesig int,
                    @htmlon int,
                    @smileyoff int,
                    @bbcodeoff int,
                    @parseurloff int,
                    @attachment int,
                    @rate int,
                    @ratetimes int

                    AS


                    DEClARE @postid int

                    DELETE FROM [dnt_postid] WHERE DATEDIFF(n, postdatetime, GETDATE()) >5

                    INSERT INTO [dnt_postid] ([postdatetime]) VALUES(GETDATE())

                    SELECT @postid=SCOPE_IDENTITY()

                    INSERT INTO [dnt_posts1]([pid], [fid], [tid], [parentid], [layer], [poster], [posterid], [title], [postdatetime], [message], [ip], [lastedit], [invisible], [usesig], [htmlon], [smileyoff], [bbcodeoff], [parseurloff], [attachment], [rate], [ratetimes]) VALUES(@postid, @fid, @tid, @parentid, @layer, @poster, @posterid, @title, @postdatetime, @message, @ip, @lastedit, @invisible, @usesig, @htmlon, @smileyoff, @bbcodeoff, @parseurloff, @attachment, @rate, @ratetimes)

                    IF @parentid=0
                        BEGIN
                    		
                            UPDATE [dnt_posts1] SET [parentid]=@postid WHERE [pid]=@postid
                        END

                    IF @@ERROR=0
                        BEGIN
                            IF  @invisible = 0
                            BEGIN
                    		
                                UPDATE [dnt_statistics] SET [totalpost]=[totalpost] + 1
                    		
                    		
                    		
                                DECLARE @fidlist AS VARCHAR(1000)
                                DECLARE @strsql AS VARCHAR(4000)
                    			
                                SET @fidlist = '';
                    			
                                SELECT @fidlist = ISNULL([parentidlist],'') FROM [dnt_forums] WHERE [fid] = @fid
                                IF RTRIM(@fidlist)<>''
	                                BEGIN
		                                SET @fidlist = RTRIM(@fidlist) + ',' + CAST(@fid AS VARCHAR(10))
	                                END
                                ELSE
	                                BEGIN
		                                SET @fidlist = CAST(@fid AS VARCHAR(10))
	                                END
                            
                    			
                                UPDATE [dnt_forums] SET 
						                                [posts]=[posts] + 1, 
						                                [todayposts]=CASE 
										                                WHEN DATEDIFF(day, [lastpost], GETDATE())=0 THEN [todayposts]*1 + 1 
									                                 ELSE 1 
									                                 END,
						                                [lasttid]=@tid,	
                                                        [lasttitle]=@topictitle,
						                                [lastpost]=@postdatetime,
						                                [lastposter]=@poster,
						                                [lastposterid]=@posterid 
                    							
				                                WHERE (CHARINDEX(',' + RTRIM([fid]) + ',', ',' + (SELECT @fidlist AS [fid]) + ',') > 0)
                    			
                    			
                                UPDATE [dnt_users] SET
	                                [lastpost] = @postdatetime,
	                                [lastpostid] = @postid,
	                                [lastposttitle] = @title,
	                                [posts] = [posts] + 1,
	                                [lastactivity] = GETDATE()
                                WHERE [uid] = @posterid
                            
                            
                                IF @layer<=0
	                                BEGIN
		                                UPDATE [dnt_topics] SET [replies]=0,[lastposter]=@poster,[lastpost]=@postdatetime,[lastposterid]=@posterid WHERE [tid]=@tid
	                                END
                                ELSE
	                                BEGIN
		                                UPDATE [dnt_topics] SET [replies]=[replies] + 1,[lastposter]=@poster,[lastpost]=@postdatetime,[lastposterid]=@posterid WHERE [tid]=@tid
	                                END
                            END

                            UPDATE [dnt_topics] SET [lastpostid]=@postid WHERE [tid]=@tid
                            
                        IF @posterid <> -1
                            BEGIN
                                INSERT [dnt_myposts]([uid], [tid], [pid], [dateline]) VALUES(@posterid, @tid, @postid, @postdatetime)
                            END

                        END
                    	
                    SELECT @postid AS postid

                    ~


                    CREATE PROCEDURE dnt_getfirstpostid
                    @tid int
                    AS
                    SELECT TOP 1 [pid] FROM [dnt_posts1] WHERE [tid]=@tid ORDER BY [pid]

                    ~


                    CREATE PROCEDURE dnt_getpostcount
                    @tid int
                    AS
                    SELECT COUNT(pid) FROM [dnt_posts1] WHERE [tid]=@tid AND [invisible]=0 AND layer>0

                    ~


                    CREATE  PROCEDURE dnt_deletepostbypid
                        @pid int,
						@chanageposts AS BIT
                    AS

                        DECLARE @fid int
                        DECLARE @tid int
                        DECLARE @posterid int
                        DECLARE @lastforumposterid int
                        DECLARE @layer int
                        DECLARE @postdatetime smalldatetime
                        DECLARE @poster varchar(50)
                        DECLARE @postcount int
                        DECLARE @title nchar(60)
                        DECLARE @lasttid int
                        DECLARE @postid int
                        DECLARE @todaycount int
                    	
                    	
                        SELECT @fid = [fid],@tid = [tid],@posterid = [posterid],@layer = [layer], @postdatetime = [postdatetime] FROM [dnt_posts1] WHERE pid = @pid

                        DECLARE @fidlist AS VARCHAR(1000)
                    	
                        SET @fidlist = '';
                    	
                        SELECT @fidlist = ISNULL([parentidlist],'') FROM [dnt_forums] WHERE [fid] = @fid
                        IF RTRIM(@fidlist)<>''
                            BEGIN
                                SET @fidlist = RTRIM(@fidlist) + ',' + CAST(@fid AS VARCHAR(10))
                            END
                        ELSE
                            BEGIN
                                SET @fidlist = CAST(@fid AS VARCHAR(10))
                            END


                        IF @layer<>0

                            BEGIN
                    			
								IF @chanageposts = 1
									BEGIN
										UPDATE [dnt_statistics] SET [totalpost]=[totalpost] - 1

										UPDATE [dnt_forums] SET 
											[posts]=[posts] - 1, 
											[todayposts]=CASE 
																WHEN DATEPART(yyyy, @postdatetime)=DATEPART(yyyy,GETDATE()) AND DATEPART(mm, @postdatetime)=DATEPART(mm,GETDATE()) AND DATEPART(dd, @postdatetime)=DATEPART(dd,GETDATE()) THEN [todayposts] - 1
																ELSE [todayposts]
														END						
										WHERE (CHARINDEX(',' + RTRIM([fid]) + ',', ',' +
															(SELECT @fidlist AS [fid]) + ',') > 0)
                    			
										UPDATE [dnt_users] SET [posts] = [posts] - 1 WHERE [uid] = @posterid

										UPDATE [dnt_topics] SET [replies]=[replies] - 1 WHERE [tid]=@tid
									END
                    			
                                DELETE FROM [dnt_posts1] WHERE [pid]=@pid
                    			
                            END
                        ELSE
                            BEGIN
                    		
                                SELECT @postcount = COUNT([pid]) FROM [dnt_posts1] WHERE [tid] = @tid
                                SELECT @todaycount = COUNT([pid]) FROM [dnt_posts1] WHERE [tid] = @tid AND DATEDIFF(d, [postdatetime], GETDATE()) = 0
                    			
								IF @chanageposts = 1
									BEGIN
										UPDATE [dnt_statistics] SET [totaltopic]=[totaltopic] - 1, [totalpost]=[totalpost] - @postcount
		                    			
										UPDATE [dnt_forums] SET [posts]=[posts] - @postcount, [topics]=[topics] - 1,[todayposts]=[todayposts] - @todaycount WHERE (CHARINDEX(',' + RTRIM([fid]) + ',', ',' +(SELECT @fidlist AS [fid]) + ',') > 0)
		                    			
										UPDATE [dnt_users] SET [posts] = [posts] - @postcount WHERE [uid] = @posterid
                    			
									END

                                DELETE FROM [dnt_posts1] WHERE [tid] = @tid
                    			
                                DELETE FROM [dnt_topics] WHERE [tid] = @tid
                    			
                            END	
                    		

                        IF @layer<>0
                            BEGIN
                                SELECT TOP 1 @pid = [pid], @posterid = [posterid], @postdatetime = [postdatetime], @title = [title], @poster = [poster] FROM [dnt_posts1] WHERE [tid]=@tid ORDER BY [pid] DESC
                                UPDATE [dnt_topics] SET [lastposter]=@poster,[lastpost]=@postdatetime,[lastpostid]=@pid,[lastposterid]=@posterid WHERE [tid]=@tid
                            END



                        SELECT @lasttid = [lasttid] FROM [dnt_forums] WHERE [fid] = @fid

                    	
                        IF @lasttid = @tid
                            BEGIN

                    			
                    			

                                SELECT TOP 1 @pid = [pid], @tid = [tid],@lastforumposterid = [posterid], @title = [title], @postdatetime = [postdatetime], @poster = [poster] FROM [dnt_posts1] WHERE [fid] = @fid ORDER BY [pid] DESC
                    			
                            
                            
                                UPDATE [dnt_forums] SET 
                    			
	                                [lastpost]=@postdatetime,
	                                [lastposter]=ISNULL(@poster,''),
	                                [lastposterid]=ISNULL(@lastforumposterid,'0')

                                WHERE (CHARINDEX(',' + RTRIM([fid]) + ',', ',' +
					                                (SELECT @fidlist AS [fid]) + ',') > 0)


                    			
                                SELECT TOP 1 @pid = [pid], @tid = [tid],@posterid = [posterid], @postdatetime = [postdatetime], @title = [title], @poster = [poster] FROM [dnt_posts1] WHERE [posterid]=@posterid ORDER BY [pid] DESC
                    			
                                UPDATE [dnt_users] SET
                    			
	                                [lastpost] = @postdatetime,
	                                [lastpostid] = @pid,
	                                [lastposttitle] = ISNULL(@title,'')
                    				
                                WHERE [uid] = @posterid
                    			
                            END


                    ~


                    CREATE PROCEDURE dnt_getposttree
                    @tid int
                    AS
                    SELECT [pid], [layer], [title], [poster], [posterid],[postdatetime],[message] FROM [dnt_posts1] WHERE [tid]=@tid AND [invisible]=0 ORDER BY [parentid];


                    ~

                    CREATE PROCEDURE dnt_getsinglepost
                    @tid int,
                    @pid int
                    AS
                    SELECT [aid], [tid], [pid], [postdatetime], [readperm], [filename], [description], [filetype], [filesize], [attachment], [downloads] FROM [dnt_attachments] WHERE [tid]=@tid

                    SELECT TOP 1 
	                                [dnt_posts1].[pid], 
	                                [dnt_posts1].[fid], 
	                                [dnt_posts1].[title], 
	                                [dnt_posts1].[layer],
	                                [dnt_posts1].[message], 
	                                [dnt_posts1].[ip], 
	                                [dnt_posts1].[lastedit], 
	                                [dnt_posts1].[postdatetime], 
	                                [dnt_posts1].[attachment], 
	                                [dnt_posts1].[poster], 
	                                [dnt_posts1].[invisible], 
	                                [dnt_posts1].[usesig], 
	                                [dnt_posts1].[htmlon], 
	                                [dnt_posts1].[smileyoff], 
	                                [dnt_posts1].[parseurloff], 
	                                [dnt_posts1].[bbcodeoff], 
	                                [dnt_posts1].[rate], 
	                                [dnt_posts1].[ratetimes], 
	                                [dnt_posts1].[posterid], 
	                                [dnt_users].[nickname],  
	                                [dnt_users].[username], 
	                                [dnt_users].[groupid],
                                    [dnt_users].[spaceid],
                                    [dnt_users].[gender],
									[dnt_users].[bday], 
	                                [dnt_users].[email], 
	                                [dnt_users].[showemail], 
	                                [dnt_users].[digestposts], 
	                                [dnt_users].[credits], 
	                                [dnt_users].[extcredits1], 
	                                [dnt_users].[extcredits2], 
	                                [dnt_users].[extcredits3], 
	                                [dnt_users].[extcredits4], 
	                                [dnt_users].[extcredits5], 
	                                [dnt_users].[extcredits6], 
	                                [dnt_users].[extcredits7], 
	                                [dnt_users].[extcredits8], 
	                                [dnt_users].[posts], 
	                                [dnt_users].[joindate], 
	                                [dnt_users].[onlinestate], 
	                                [dnt_users].[lastactivity], 
	                                [dnt_users].[invisible], 
	                                [dnt_userfields].[avatar], 
	                                [dnt_userfields].[avatarwidth], 
	                                [dnt_userfields].[avatarheight], 
	                                [dnt_userfields].[medals], 
	                                [dnt_userfields].[sightml] AS signature, 
	                                [dnt_userfields].[location], 
	                                [dnt_userfields].[customstatus], 
	                                [dnt_userfields].[website], 
	                                [dnt_userfields].[icq], 
	                                [dnt_userfields].[qq], 
	                                [dnt_userfields].[msn], 
	                                [dnt_userfields].[yahoo], 
	                                [dnt_userfields].[skype] 
                    FROM [dnt_posts1] LEFT JOIN [dnt_users] ON [dnt_users].[uid]=[dnt_posts1].[posterid] LEFT JOIN [dnt_userfields] ON [dnt_userfields].[uid]=[dnt_users].[uid] WHERE [dnt_posts1].[pid]=@pid


                    ~

                    CREATE PROCEDURE dnt_updatepost
                        @pid int,
                        @title nvarchar(160),
                        @message ntext,
                        @lastedit nvarchar(50),
                        @invisible int,
                        @usesig int,
                        @htmlon int,
                        @smileyoff int,
                        @bbcodeoff int,
                        @parseurloff int
                    AS
                    UPDATE dnt_posts1 SET 
                        [title]=@title,
                        [message]=@message,
                        [lastedit]=@lastedit,
                        [invisible]=@invisible,
                        [usesig]=@usesig,
                        [htmlon]=@htmlon,
                        [smileyoff]=@smileyoff,
                        [bbcodeoff]=@bbcodeoff,
                        [parseurloff]=@parseurloff WHERE [pid]=@pid


                    ~

                    CREATE PROCEDURE dnt_getnewtopics 
                    @fidlist VARCHAR(500)
                    AS
                    IF @fidlist<>''
                    BEGIN
                    DECLARE @strSQL VARCHAR(5000)
                    SET @strSQL = 'SELECT TOP 20   [dnt_posts1].[tid], [dnt_posts1].[title], [dnt_posts1].[poster], [dnt_posts1].[postdatetime], [dnt_posts1].[message],[dnt_forums].[name] FROM [dnt_posts1]  LEFT JOIN [dnt_forums] ON [dnt_posts1].[fid]=[dnt_forums].[fid] WHERE  [dnt_forums].[fid] NOT IN ('+@fidlist +')  AND [dnt_posts1].[layer]=0 ORDER BY [dnt_posts1].[pid] DESC' 
                    END
                    ELSE
                    BEGIN
                    SET @strSQL = 'SELECT TOP 20   [dnt_posts1].[tid], [dnt_posts1].[title], [dnt_posts1].[poster], [dnt_posts1].[postdatetime], [dnt_posts1].[message],[dnt_forums].[name] FROM [dnt_posts1]  LEFT JOIN [dnt_forums] ON [dnt_posts1].[fid]=[dnt_forums].[fid] WHERE [dnt_posts1].[layer]=0 ORDER BY [dnt_posts1].[pid] DESC'
                    END
                    EXEC(@strSQL)

               ");

            sbProc.Replace("\"", "'").Replace("dnt_posts1", MainForm.cic.TargetDbTablePrefix + "posts" + PostsTableId);
            sbProc.Replace("maxtablelistid", PostsTableId.ToString());
            sbProc.Replace("dnt_createpost", MainForm.cic.TargetDbTablePrefix + "createpost" + PostsTableId);
            sbProc.Replace("dnt_getfirstpostid", MainForm.cic.TargetDbTablePrefix + "getfirstpost" + PostsTableId + "id");
            sbProc.Replace("dnt_getpostcount", MainForm.cic.TargetDbTablePrefix + "getpost" + PostsTableId + "count");
            sbProc.Replace("dnt_deletepostbypid", MainForm.cic.TargetDbTablePrefix + "deletepost" + PostsTableId + "bypid");
            sbProc.Replace("dnt_getposttree", MainForm.cic.TargetDbTablePrefix + "getpost" + PostsTableId + "tree");
            sbProc.Replace("dnt_getsinglepost", MainForm.cic.TargetDbTablePrefix + "getsinglepost" + PostsTableId);
            sbProc.Replace("dnt_updatepost", MainForm.cic.TargetDbTablePrefix + "updatepost" + PostsTableId);
            sbProc.Replace("dnt_getnewtopics", MainForm.cic.TargetDbTablePrefix + "getnewtopics");
            sbProc.Replace("dnt_", MainForm.cic.TargetDbTablePrefix);
            #endregion
            //执行创建语句
            string sqlProc = sbProc.ToString();
            DBHelper CreateProcDBH = MainForm.GetTargetDBH();
            CreateProcDBH.Open();
            foreach (string sql in sqlProc.Split('~'))
            {
                CreateProcDBH.ExecuteNonQuery(sql);
            }
            CreateProcDBH.Dispose();

            //dnt_tablelist添加记录
            DBHelper AddNewTableRecord = MainForm.GetTargetDBH();
            AddNewTableRecord.Open();
            AddNewTableRecord.SetIdentityInsertON(string.Format("{0}tablelist", MainForm.cic.TargetDbTablePrefix));
            AddNewTableRecord.ExecuteNonQuery(string.Format("INSERT INTO {0}tablelist (id,description,mintid,maxtid) VALUES({1}, '{1}_by NConvert', {2}, {3})", MainForm.cic.TargetDbTablePrefix, PostsTableId, mintid, maxtid));
            AddNewTableRecord.Dispose();
        }

        /// <summary>
        /// 返回帖子所在的分表id
        /// </summary>
        /// <param name="tid">帖子的主题id</param>
        /// <returns>帖子所在的分表</returns>
        public static int GetPostTableId(int tid)
        {
            PostTables objPostTable = MainForm.PostTableList.Find(delegate(PostTables p) { return p.mintid <= tid && (p.maxtid <= 0 || p.maxtid >= tid); });
            if (objPostTable != null)
            {
                return objPostTable.id;
            }
            return 1;
            //throw new Exception(string.Format("没有找到分表。tid={0}", tid));
        }

        /// <summary>
        /// 正则替换
        /// </summary>
        /// <param name="pattern">替换规则</param>
        /// <param name="input">原始字符串</param>
        /// <param name="replacement">替换为</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceRegex(string pattern, string input, string replacement)
        {
            // Regex search and replace
            RegexOptions options = RegexOptions.IgnoreCase;
            Regex regex = new Regex(pattern, options);
            return regex.Replace(input, replacement);
        }        
    }
}
