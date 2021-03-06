﻿//using Dotmim.Sync.MySql;
//using Dotmim.Sync.Tests.Misc;
//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Xunit;

//namespace Dotmim.Sync.Tests.Old.SqlServer
//{
//    public class WordPressFixture : IDisposable
//    {
//        private string serverDbName = "wordpress";
//        private string clientDbName = "wordpress_client";

//        public string[] Tables => new string[] { "wp_users", "wp_usermeta", "wp_terms", "wp_termmeta", "wp_term_taxonomy",
//                                        "wp_term_relationships", "wp_posts", "wp_postmeta", "wp_options", "wp_links",
//                                        "wp_comments", "wp_commentmeta"};


//        public String ServerConnectionString => HelperDB.GetMySqlDatabaseConnectionString(serverDbName);
//        public String ClientMySqlConnectionString => HelperDB.GetMySqlDatabaseConnectionString(clientDbName);

//        public SyncAgent Agent { get; set; }


//        public WordPressFixture()
//        {
//            // create client database
//            HelperDB.DropMySqlDatabase(clientDbName);
//            HelperDB.CreateMySqlDatabase(clientDbName);

//            HelperDB.DropMySqlDatabase(serverDbName);
//            HelperDB.CreateMySqlDatabase(serverDbName);

//            // restore server database
//            var wordpressscript = Path.Combine(Directory.GetCurrentDirectory(), "Backup", "Wordpress.sql");
//            var fs = File.OpenText(wordpressscript);
//            var script = fs.ReadToEnd();
//            HelperDB.ExecuteMySqlScript(serverDbName, script);

//            var serverProvider = new MySqlSyncProvider(ServerConnectionString);
//            var clientProvider = new MySqlSyncProvider(ClientMySqlConnectionString);

//            Agent = new SyncAgent(clientProvider, serverProvider, Tables);

//        }
//        public void Dispose()
//        {
//            HelperDB.DropSqlDatabase(serverDbName);
//            HelperDB.DropMySqlDatabase(clientDbName);

//        }
//    }

//    [TestCaseOrderer("Dotmim.Sync.Tests.Misc.PriorityOrderer", "Dotmim.Sync.Tests")]
//    public class WordPressTests : IClassFixture<WordPressFixture>
//    {
//        WordPressFixture fixture;
//        SyncAgent agent;

//        public WordPressTests(WordPressFixture fixture)
//        {
//            this.fixture = fixture;
//            this.agent = fixture.Agent;
//        }

//        [Fact, TestPriority(1)]
//        public async Task Initialize()
//        {
//            var session = await agent.SynchronizeAsync();

//            Assert.Equal(141, session.TotalChangesDownloaded);
//            Assert.Equal(0, session.TotalChangesUploaded);
//        }

//    }
//}
