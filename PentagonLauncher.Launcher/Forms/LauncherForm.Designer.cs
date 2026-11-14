using System;
using System.Drawing;
using System.Windows.Forms;
using PentagonLauncher.Launcher.Controls;

namespace PentagonLauncher.Launcher.Forms
{


    public partial class LauncherForm
    {
        private System.ComponentModel.IContainer components = null;
        private CaroLogo logoBrand = null!;
        private Label lblBrand = null!;
        private Label lblBrandSub = null!;
        private GlassPanel profileChip = null!;
        private Label lblProfileDot = null!;
        private Label lblProfileCaption = null!;
        private Label lblUsername = null!;
        private Label lblGreeting = null!;
        private Label lblTopSub = null!;
        private NeonButton btnQuick = null!;
        private NeonButton btnRooms = null!;
        private NeonButton btnLeaderboard = null!;
        private NeonButton btnAccountSide = null!;
        private GlassPanel infoPanel = null!;
        private Label lblInfoHeading = null!;
        private Label lblInfoBody = null!;
        private GlassPanel rankPanel = null!;
        private Label lblRankTitle = null!;
        private Label lblRankHeader = null!;
        private Label lblRank1 = null!;
        private Label lblRank1Score = null!;
        private Label lblRank2 = null!;
        private Label lblRank2Score = null!;
        private Label lblRank3 = null!;
        private Label lblRank3Score = null!;
        private GlassPanel roomPanel = null!;
        private Label lblRoomsTitle = null!;
        private Label lblRoomsHint = null!;
        private Label lblRoom1 = null!;
        private Label lblCount1 = null!;
        private NeonButton btnJoin1 = null!;
        private Label lblRoom2 = null!;
        private Label lblCount2 = null!;
        private NeonButton btnJoin2 = null!;
        private Label lblRoom3 = null!;
        private Label lblCount3 = null!;
        private NeonButton btnJoin3 = null!;
        private NeonButton btnLogoutSide = null!;
        private Label lblBuild = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing) components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            logoBrand = new CaroLogo();
            lblBrand = new Label();
            lblBrandSub = new Label();
            profileChip = new GlassPanel();
            lblProfileDot = new Label();
            lblProfileCaption = new Label();
            lblUsername = new Label();
            lblGreeting = new Label();
            lblTopSub = new Label();
            btnQuick = new NeonButton();
            btnRooms = new NeonButton();
            btnLeaderboard = new NeonButton();
            btnAccountSide = new NeonButton();
            infoPanel = new GlassPanel();
            lblInfoHeading = new Label();
            lblInfoBody = new Label();
            rankPanel = new GlassPanel();
            lblRankTitle = new Label();
            lblRankHeader = new Label();
            lblRank1 = new Label();
            lblRank1Score = new Label();
            lblRank2 = new Label();
            lblRank2Score = new Label();
            lblRank3 = new Label();
            lblRank3Score = new Label();
            roomPanel = new GlassPanel();
            lblRoomsTitle = new Label();
            lblRoomsHint = new Label();
            lblRoom1 = new Label();
            lblCount1 = new Label();
            btnJoin1 = new NeonButton();
            lblRoom2 = new Label();
            lblCount2 = new Label();
            btnJoin2 = new NeonButton();
            lblRoom3 = new Label();
            lblCount3 = new Label();
            btnJoin3 = new NeonButton();
            btnLogoutSide = new NeonButton();
            lblBuild = new Label();
            profileChip.SuspendLayout();
            infoPanel.SuspendLayout();
            rankPanel.SuspendLayout();
            roomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // logoBrand
            // 
            logoBrand.BackColor = Color.Transparent;
            logoBrand.GridColor = Color.FromArgb(103, 109, 206);
            logoBrand.Location = new Point(31, 19);
            logoBrand.MinimumSize = new Size(28, 28);
            logoBrand.Name = "logoBrand";
            logoBrand.OColor = Color.FromArgb(245, 128, 222);
            logoBrand.Size = new Size(72, 72);
            logoBrand.TabIndex = 0;
            logoBrand.XColor = Color.FromArgb(50, 214, 255);
            // 
            // lblBrand
            // 
            lblBrand.BackColor = Color.Transparent;
            lblBrand.Font = new Font("Segoe UI", 27F, FontStyle.Bold);
            lblBrand.ForeColor = Color.FromArgb(246, 244, 255);
            lblBrand.Location = new Point(115, 24);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(420, 43);
            lblBrand.TabIndex = 1;
            lblBrand.Text = "Caro Online";
            lblBrand.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblBrandSub
            // 
            lblBrandSub.BackColor = Color.Transparent;
            lblBrandSub.Font = new Font("Segoe UI", 10F);
            lblBrandSub.ForeColor = Color.FromArgb(170, 168, 205);
            lblBrandSub.Location = new Point(116, 66);
            lblBrandSub.Name = "lblBrandSub";
            lblBrandSub.Size = new Size(470, 28);
            lblBrandSub.TabIndex = 2;
            lblBrandSub.Text = "Chơi Caro trực tuyến cùng bạn bè";
            lblBrandSub.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // profileChip
            // 
            profileChip.BackColor = Color.FromArgb(27, 27, 66);
            profileChip.BorderColor = Color.FromArgb(103, 91, 158);
            profileChip.Controls.Add(lblProfileDot);
            profileChip.Controls.Add(lblProfileCaption);
            profileChip.Controls.Add(lblUsername);
            profileChip.CornerRadius = 16;
            profileChip.GradientColor = Color.FromArgb(27, 27, 66);
            profileChip.Location = new Point(830, 24);
            profileChip.Name = "profileChip";
            profileChip.Padding = new Padding(1);
            profileChip.Size = new Size(257, 66);
            profileChip.TabIndex = 3;
            // 
            // lblProfileDot
            // 
            lblProfileDot.BackColor = Color.Transparent;
            lblProfileDot.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblProfileDot.ForeColor = Color.FromArgb(116, 236, 182);
            lblProfileDot.Location = new Point(18, 18);
            lblProfileDot.Name = "lblProfileDot";
            lblProfileDot.Size = new Size(24, 29);
            lblProfileDot.TabIndex = 0;
            lblProfileDot.Text = "●";
            lblProfileDot.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblProfileCaption
            // 
            lblProfileCaption.BackColor = Color.Transparent;
            lblProfileCaption.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblProfileCaption.ForeColor = Color.FromArgb(170, 168, 205);
            lblProfileCaption.Location = new Point(54, 9);
            lblProfileCaption.Name = "lblProfileCaption";
            lblProfileCaption.Size = new Size(180, 22);
            lblProfileCaption.TabIndex = 1;
            lblProfileCaption.Text = "XIN CHÀO,";
            lblProfileCaption.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUsername
            // 
            lblUsername.BackColor = Color.Transparent;
            lblUsername.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblUsername.ForeColor = Color.FromArgb(246, 244, 255);
            lblUsername.Location = new Point(54, 30);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(185, 28);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "demo";
            lblUsername.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGreeting
            // 
            lblGreeting.BackColor = Color.Transparent;
            lblGreeting.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblGreeting.ForeColor = Color.FromArgb(246, 244, 255);
            lblGreeting.Location = new Point(33, 133);
            lblGreeting.Name = "lblGreeting";
            lblGreeting.Size = new Size(509, 49);
            lblGreeting.TabIndex = 4;
            lblGreeting.Text = "Bắt đầu cuộc chơi";
            lblGreeting.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTopSub
            // 
            lblTopSub.BackColor = Color.Transparent;
            lblTopSub.Font = new Font("Segoe UI", 10F);
            lblTopSub.ForeColor = Color.FromArgb(170, 168, 205);
            lblTopSub.Location = new Point(35, 183);
            lblTopSub.Name = "lblTopSub";
            lblTopSub.Size = new Size(490, 30);
            lblTopSub.TabIndex = 5;
            lblTopSub.Text = "Chọn chế độ và thách đấu bạn bè ngay hôm nay.";
            lblTopSub.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnQuick
            // 
            btnQuick.BackColor = Color.FromArgb(49, 190, 235);
            btnQuick.BorderColor = Color.FromArgb(173, 141, 255);
            btnQuick.CornerRadius = 12;
            btnQuick.FlatStyle = FlatStyle.Flat;
            btnQuick.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnQuick.ForeColor = Color.FromArgb(246, 244, 255);
            btnQuick.GradientColor = Color.FromArgb(146, 86, 237);
            btnQuick.Location = new Point(34, 239);
            btnQuick.Name = "btnQuick";
            btnQuick.Size = new Size(250, 112);
            btnQuick.TabIndex = 0;
            btnQuick.Text = "▶   Chơi nhanh";
            btnQuick.UseVisualStyleBackColor = false;
            btnQuick.Click += btnQuick_Click;
            // 
            // btnRooms
            // 
            btnRooms.BackColor = Color.FromArgb(49, 39, 93);
            btnRooms.BorderColor = Color.FromArgb(101, 89, 158);
            btnRooms.CornerRadius = 12;
            btnRooms.FlatStyle = FlatStyle.Flat;
            btnRooms.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnRooms.ForeColor = Color.FromArgb(246, 244, 255);
            btnRooms.GradientColor = Color.FromArgb(33, 29, 72);
            btnRooms.Location = new Point(301, 239);
            btnRooms.Name = "btnRooms";
            btnRooms.Size = new Size(250, 112);
            btnRooms.TabIndex = 1;
            btnRooms.Text = "Phòng chơi";
            btnRooms.UseVisualStyleBackColor = false;
            btnRooms.Click += btnRooms_Click;
            // 
            // btnLeaderboard
            // 
            btnLeaderboard.BackColor = Color.FromArgb(49, 39, 93);
            btnLeaderboard.BorderColor = Color.FromArgb(101, 89, 158);
            btnLeaderboard.CornerRadius = 12;
            btnLeaderboard.FlatStyle = FlatStyle.Flat;
            btnLeaderboard.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            btnLeaderboard.ForeColor = Color.FromArgb(246, 244, 255);
            btnLeaderboard.GradientColor = Color.FromArgb(33, 29, 72);
            btnLeaderboard.Location = new Point(34, 370);
            btnLeaderboard.Name = "btnLeaderboard";
            btnLeaderboard.Size = new Size(250, 112);
            btnLeaderboard.TabIndex = 2;
            btnLeaderboard.Text = "★   Bảng xếp hạng";
            btnLeaderboard.UseVisualStyleBackColor = false;
            btnLeaderboard.Click += btnLeaderboard_Click;
            // 
            // btnAccountSide
            // 
            btnAccountSide.BackColor = Color.FromArgb(49, 39, 93);
            btnAccountSide.BorderColor = Color.FromArgb(101, 89, 158);
            btnAccountSide.CornerRadius = 12;
            btnAccountSide.FlatStyle = FlatStyle.Flat;
            btnAccountSide.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnAccountSide.ForeColor = Color.FromArgb(246, 244, 255);
            btnAccountSide.GradientColor = Color.FromArgb(33, 29, 72);
            btnAccountSide.Location = new Point(301, 370);
            btnAccountSide.Name = "btnAccountSide";
            btnAccountSide.Size = new Size(250, 112);
            btnAccountSide.TabIndex = 3;
            btnAccountSide.Text = "Tài khoản";
            btnAccountSide.UseVisualStyleBackColor = false;
            btnAccountSide.Click += btnAccount_Click;
            // 
            // infoPanel
            // 
            infoPanel.BackColor = Color.FromArgb(27, 27, 66);
            infoPanel.BorderColor = Color.FromArgb(96, 85, 143);
            infoPanel.Controls.Add(lblInfoHeading);
            infoPanel.Controls.Add(lblInfoBody);
            infoPanel.CornerRadius = 15;
            infoPanel.GradientColor = Color.FromArgb(27, 27, 66);
            infoPanel.Location = new Point(34, 512);
            infoPanel.Name = "infoPanel";
            infoPanel.Padding = new Padding(1);
            infoPanel.Size = new Size(518, 118);
            infoPanel.TabIndex = 6;
            // 
            // lblInfoHeading
            // 
            lblInfoHeading.BackColor = Color.Transparent;
            lblInfoHeading.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblInfoHeading.ForeColor = Color.FromArgb(64, 217, 255);
            lblInfoHeading.Location = new Point(22, 17);
            lblInfoHeading.Name = "lblInfoHeading";
            lblInfoHeading.Size = new Size(470, 26);
            lblInfoHeading.TabIndex = 0;
            lblInfoHeading.Text = "THÁCH ĐẤU • KẾT NỐI • THĂNG HẠNG";
            lblInfoHeading.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblInfoBody
            // 
            lblInfoBody.BackColor = Color.Transparent;
            lblInfoBody.Font = new Font("Segoe UI", 10.4F);
            lblInfoBody.ForeColor = Color.FromArgb(246, 244, 255);
            lblInfoBody.Location = new Point(22, 45);
            lblInfoBody.Name = "lblInfoBody";
            lblInfoBody.Size = new Size(466, 52);
            lblInfoBody.TabIndex = 1;
            lblInfoBody.Text = "Chơi cùng bạn bè, khám phá phòng chơi và rèn\nluyện chiến thuật với cộng đồng Caro Online.";
            lblInfoBody.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // rankPanel
            // 
            rankPanel.BackColor = Color.FromArgb(27, 27, 66);
            rankPanel.BorderColor = Color.FromArgb(96, 85, 143);
            rankPanel.Controls.Add(lblRankTitle);
            rankPanel.Controls.Add(lblRankHeader);
            rankPanel.Controls.Add(lblRank1);
            rankPanel.Controls.Add(lblRank1Score);
            rankPanel.Controls.Add(lblRank2);
            rankPanel.Controls.Add(lblRank2Score);
            rankPanel.Controls.Add(lblRank3);
            rankPanel.Controls.Add(lblRank3Score);
            rankPanel.CornerRadius = 17;
            rankPanel.GradientColor = Color.FromArgb(27, 27, 66);
            rankPanel.Location = new Point(577, 133);
            rankPanel.Name = "rankPanel";
            rankPanel.Padding = new Padding(1);
            rankPanel.Size = new Size(511, 232);
            rankPanel.TabIndex = 7;
            // 
            // lblRankTitle
            // 
            lblRankTitle.BackColor = Color.Transparent;
            lblRankTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblRankTitle.ForeColor = Color.FromArgb(246, 244, 255);
            lblRankTitle.Location = new Point(21, 17);
            lblRankTitle.Name = "lblRankTitle";
            lblRankTitle.Size = new Size(460, 31);
            lblRankTitle.TabIndex = 0;
            lblRankTitle.Text = "★  Bảng xếp hạng";
            lblRankTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRankHeader
            // 
            lblRankHeader.BackColor = Color.Transparent;
            lblRankHeader.Font = new Font("Segoe UI", 9.3F, FontStyle.Bold);
            lblRankHeader.ForeColor = Color.FromArgb(64, 217, 255);
            lblRankHeader.Location = new Point(23, 58);
            lblRankHeader.Name = "lblRankHeader";
            lblRankHeader.Size = new Size(465, 25);
            lblRankHeader.TabIndex = 1;
            lblRankHeader.Text = "TOP NGƯỜI CHƠI";
            lblRankHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRank1
            // 
            lblRank1.BackColor = Color.Transparent;
            lblRank1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRank1.ForeColor = Color.FromArgb(246, 244, 255);
            lblRank1.Location = new Point(27, 91);
            lblRank1.Name = "lblRank1";
            lblRank1.Size = new Size(327, 31);
            lblRank1.TabIndex = 2;
            lblRank1.Text = "01     MinhKhoa";
            lblRank1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRank1Score
            // 
            lblRank1Score.BackColor = Color.Transparent;
            lblRank1Score.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblRank1Score.ForeColor = Color.FromArgb(241, 142, 223);
            lblRank1Score.Location = new Point(404, 91);
            lblRank1Score.Name = "lblRank1Score";
            lblRank1Score.Size = new Size(85, 31);
            lblRank1Score.TabIndex = 3;
            lblRank1Score.Text = "1.560";
            lblRank1Score.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblRank2
            // 
            lblRank2.BackColor = Color.Transparent;
            lblRank2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRank2.ForeColor = Color.FromArgb(246, 244, 255);
            lblRank2.Location = new Point(27, 130);
            lblRank2.Name = "lblRank2";
            lblRank2.Size = new Size(327, 31);
            lblRank2.TabIndex = 4;
            lblRank2.Text = "02     ThanhHien";
            lblRank2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRank2Score
            // 
            lblRank2Score.BackColor = Color.Transparent;
            lblRank2Score.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblRank2Score.ForeColor = Color.FromArgb(64, 217, 255);
            lblRank2Score.Location = new Point(404, 130);
            lblRank2Score.Name = "lblRank2Score";
            lblRank2Score.Size = new Size(85, 31);
            lblRank2Score.TabIndex = 5;
            lblRank2Score.Text = "1.480";
            lblRank2Score.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblRank3
            // 
            lblRank3.BackColor = Color.Transparent;
            lblRank3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRank3.ForeColor = Color.FromArgb(246, 244, 255);
            lblRank3.Location = new Point(27, 169);
            lblRank3.Name = "lblRank3";
            lblRank3.Size = new Size(327, 31);
            lblRank3.TabIndex = 6;
            lblRank3.Text = "03     BaoNam";
            lblRank3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRank3Score
            // 
            lblRank3Score.BackColor = Color.Transparent;
            lblRank3Score.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblRank3Score.ForeColor = Color.FromArgb(170, 168, 205);
            lblRank3Score.Location = new Point(404, 169);
            lblRank3Score.Name = "lblRank3Score";
            lblRank3Score.Size = new Size(85, 31);
            lblRank3Score.TabIndex = 7;
            lblRank3Score.Text = "1.390";
            lblRank3Score.TextAlign = ContentAlignment.MiddleRight;
            // 
            // roomPanel
            // 
            roomPanel.BackColor = Color.FromArgb(27, 27, 66);
            roomPanel.BorderColor = Color.FromArgb(96, 85, 143);
            roomPanel.Controls.Add(lblRoomsTitle);
            roomPanel.Controls.Add(lblRoomsHint);
            roomPanel.Controls.Add(lblRoom1);
            roomPanel.Controls.Add(lblCount1);
            roomPanel.Controls.Add(btnJoin1);
            roomPanel.Controls.Add(lblRoom2);
            roomPanel.Controls.Add(lblCount2);
            roomPanel.Controls.Add(btnJoin2);
            roomPanel.Controls.Add(lblRoom3);
            roomPanel.Controls.Add(lblCount3);
            roomPanel.Controls.Add(btnJoin3);
            roomPanel.CornerRadius = 17;
            roomPanel.GradientColor = Color.FromArgb(27, 27, 66);
            roomPanel.Location = new Point(577, 381);
            roomPanel.Name = "roomPanel";
            roomPanel.Padding = new Padding(1);
            roomPanel.Size = new Size(511, 249);
            roomPanel.TabIndex = 8;
            // 
            // lblRoomsTitle
            // 
            lblRoomsTitle.BackColor = Color.Transparent;
            lblRoomsTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblRoomsTitle.ForeColor = Color.FromArgb(246, 244, 255);
            lblRoomsTitle.Location = new Point(22, 20);
            lblRoomsTitle.Name = "lblRoomsTitle";
            lblRoomsTitle.Size = new Size(464, 34);
            lblRoomsTitle.TabIndex = 0;
            lblRoomsTitle.Text = "Phòng đang chờ (3)";
            lblRoomsTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRoomsHint
            // 
            lblRoomsHint.BackColor = Color.Transparent;
            lblRoomsHint.Font = new Font("Segoe UI", 9.5F);
            lblRoomsHint.ForeColor = Color.FromArgb(170, 168, 205);
            lblRoomsHint.Location = new Point(24, 61);
            lblRoomsHint.Name = "lblRoomsHint";
            lblRoomsHint.Size = new Size(454, 28);
            lblRoomsHint.TabIndex = 1;
            lblRoomsHint.Text = "Chọn phòng để bắt đầu kết nối";
            lblRoomsHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblRoom1
            // 
            lblRoom1.BackColor = Color.Transparent;
            lblRoom1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRoom1.ForeColor = Color.FromArgb(246, 244, 255);
            lblRoom1.Location = new Point(24, 94);
            lblRoom1.Name = "lblRoom1";
            lblRoom1.Size = new Size(251, 31);
            lblRoom1.TabIndex = 2;
            lblRoom1.Text = "Phòng #102";
            lblRoom1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCount1
            // 
            lblCount1.BackColor = Color.Transparent;
            lblCount1.Font = new Font("Segoe UI", 10F);
            lblCount1.ForeColor = Color.FromArgb(64, 217, 255);
            lblCount1.Location = new Point(301, 94);
            lblCount1.Name = "lblCount1";
            lblCount1.Size = new Size(59, 31);
            lblCount1.TabIndex = 3;
            lblCount1.Text = "1/2";
            lblCount1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnJoin1
            // 
            btnJoin1.BackColor = Color.FromArgb(49, 39, 93);
            btnJoin1.BorderColor = Color.FromArgb(101, 89, 158);
            btnJoin1.CornerRadius = 12;
            btnJoin1.FlatStyle = FlatStyle.Flat;
            btnJoin1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnJoin1.ForeColor = Color.FromArgb(246, 244, 255);
            btnJoin1.GradientColor = Color.FromArgb(33, 29, 72);
            btnJoin1.Location = new Point(386, 94);
            btnJoin1.Name = "btnJoin1";
            btnJoin1.Size = new Size(96, 31);
            btnJoin1.TabIndex = 4;
            btnJoin1.Text = "Vào";
            btnJoin1.UseVisualStyleBackColor = false;
            btnJoin1.Click += btnRooms_Click;
            // 
            // lblRoom2
            // 
            lblRoom2.BackColor = Color.Transparent;
            lblRoom2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRoom2.ForeColor = Color.FromArgb(246, 244, 255);
            lblRoom2.Location = new Point(24, 139);
            lblRoom2.Name = "lblRoom2";
            lblRoom2.Size = new Size(251, 31);
            lblRoom2.TabIndex = 5;
            lblRoom2.Text = "Phòng #107";
            lblRoom2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCount2
            // 
            lblCount2.BackColor = Color.Transparent;
            lblCount2.Font = new Font("Segoe UI", 10F);
            lblCount2.ForeColor = Color.FromArgb(64, 217, 255);
            lblCount2.Location = new Point(301, 139);
            lblCount2.Name = "lblCount2";
            lblCount2.Size = new Size(59, 31);
            lblCount2.TabIndex = 6;
            lblCount2.Text = "1/2";
            lblCount2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnJoin2
            // 
            btnJoin2.BackColor = Color.FromArgb(49, 39, 93);
            btnJoin2.BorderColor = Color.FromArgb(101, 89, 158);
            btnJoin2.CornerRadius = 12;
            btnJoin2.FlatStyle = FlatStyle.Flat;
            btnJoin2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnJoin2.ForeColor = Color.FromArgb(246, 244, 255);
            btnJoin2.GradientColor = Color.FromArgb(33, 29, 72);
            btnJoin2.Location = new Point(386, 139);
            btnJoin2.Name = "btnJoin2";
            btnJoin2.Size = new Size(96, 31);
            btnJoin2.TabIndex = 5;
            btnJoin2.Text = "Vào";
            btnJoin2.UseVisualStyleBackColor = false;
            btnJoin2.Click += btnRooms_Click;
            // 
            // lblRoom3
            // 
            lblRoom3.BackColor = Color.Transparent;
            lblRoom3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRoom3.ForeColor = Color.FromArgb(246, 244, 255);
            lblRoom3.Location = new Point(24, 184);
            lblRoom3.Name = "lblRoom3";
            lblRoom3.Size = new Size(251, 31);
            lblRoom3.TabIndex = 7;
            lblRoom3.Text = "Phòng #115";
            lblRoom3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCount3
            // 
            lblCount3.BackColor = Color.Transparent;
            lblCount3.Font = new Font("Segoe UI", 10F);
            lblCount3.ForeColor = Color.FromArgb(64, 217, 255);
            lblCount3.Location = new Point(301, 184);
            lblCount3.Name = "lblCount3";
            lblCount3.Size = new Size(59, 31);
            lblCount3.TabIndex = 8;
            lblCount3.Text = "2/2";
            lblCount3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnJoin3
            // 
            btnJoin3.BackColor = Color.FromArgb(49, 39, 93);
            btnJoin3.BorderColor = Color.FromArgb(101, 89, 158);
            btnJoin3.CornerRadius = 12;
            btnJoin3.FlatStyle = FlatStyle.Flat;
            btnJoin3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnJoin3.ForeColor = Color.FromArgb(246, 244, 255);
            btnJoin3.GradientColor = Color.FromArgb(33, 29, 72);
            btnJoin3.Location = new Point(386, 184);
            btnJoin3.Name = "btnJoin3";
            btnJoin3.Size = new Size(96, 31);
            btnJoin3.TabIndex = 6;
            btnJoin3.Text = "Vào";
            btnJoin3.UseVisualStyleBackColor = false;
            btnJoin3.Click += btnRooms_Click;
            // 
            // btnLogoutSide
            // 
            btnLogoutSide.BackColor = Color.FromArgb(49, 39, 93);
            btnLogoutSide.BorderColor = Color.FromArgb(101, 89, 158);
            btnLogoutSide.CornerRadius = 12;
            btnLogoutSide.FlatStyle = FlatStyle.Flat;
            btnLogoutSide.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogoutSide.ForeColor = Color.FromArgb(246, 244, 255);
            btnLogoutSide.GradientColor = Color.FromArgb(33, 29, 72);
            btnLogoutSide.Location = new Point(33, 644);
            btnLogoutSide.Name = "btnLogoutSide";
            btnLogoutSide.Size = new Size(178, 32);
            btnLogoutSide.TabIndex = 7;
            btnLogoutSide.Text = "↪  Đăng xuất";
            btnLogoutSide.UseVisualStyleBackColor = false;
            btnLogoutSide.Click += btnLogout_Click;
            // 
            // lblBuild
            // 
            lblBuild.BackColor = Color.Transparent;
            lblBuild.Font = new Font("Segoe UI", 9F);
            lblBuild.ForeColor = Color.FromArgb(170, 168, 205);
            lblBuild.Location = new Point(680, 646);
            lblBuild.Name = "lblBuild";
            lblBuild.Size = new Size(407, 26);
            lblBuild.TabIndex = 9;
            lblBuild.Text = "CARO ONLINE   /   DESIGNER EDITION V4";
            lblBuild.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LauncherForm
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(16, 16, 44);
            ClientSize = new Size(1120, 690);
            Controls.Add(logoBrand);
            Controls.Add(lblBrand);
            Controls.Add(lblBrandSub);
            Controls.Add(profileChip);
            Controls.Add(lblGreeting);
            Controls.Add(lblTopSub);
            Controls.Add(btnQuick);
            Controls.Add(btnRooms);
            Controls.Add(btnLeaderboard);
            Controls.Add(btnAccountSide);
            Controls.Add(infoPanel);
            Controls.Add(rankPanel);
            Controls.Add(roomPanel);
            Controls.Add(btnLogoutSide);
            Controls.Add(lblBuild);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "LauncherForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Caro Online  ·  Launcher";
            profileChip.ResumeLayout(false);
            infoPanel.ResumeLayout(false);
            rankPanel.ResumeLayout(false);
            roomPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
