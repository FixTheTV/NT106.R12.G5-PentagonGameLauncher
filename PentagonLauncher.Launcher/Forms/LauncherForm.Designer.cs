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
            this.SuspendLayout();
            this.logoBrand = new CaroLogo();
            this.lblBrand = new Label();
            this.lblBrandSub = new Label();
            this.profileChip = new GlassPanel();
            this.lblProfileDot = new Label();
            this.lblProfileCaption = new Label();
            this.lblUsername = new Label();
            this.lblGreeting = new Label();
            this.lblTopSub = new Label();
            this.btnQuick = new NeonButton();
            this.btnRooms = new NeonButton();
            this.btnLeaderboard = new NeonButton();
            this.btnAccountSide = new NeonButton();
            this.infoPanel = new GlassPanel();
            this.lblInfoHeading = new Label();
            this.lblInfoBody = new Label();
            this.rankPanel = new GlassPanel();
            this.lblRankTitle = new Label();
            this.lblRankHeader = new Label();
            this.lblRank1 = new Label();
            this.lblRank1Score = new Label();
            this.lblRank2 = new Label();
            this.lblRank2Score = new Label();
            this.lblRank3 = new Label();
            this.lblRank3Score = new Label();
            this.roomPanel = new GlassPanel();
            this.lblRoomsTitle = new Label();
            this.lblRoomsHint = new Label();
            this.lblRoom1 = new Label();
            this.lblCount1 = new Label();
            this.btnJoin1 = new NeonButton();
            this.lblRoom2 = new Label();
            this.lblCount2 = new Label();
            this.btnJoin2 = new NeonButton();
            this.lblRoom3 = new Label();
            this.lblCount3 = new Label();
            this.btnJoin3 = new NeonButton();
            this.btnLogoutSide = new NeonButton();
            this.lblBuild = new Label();

            // logoBrand: editable from Visual Studio Designer.
            this.logoBrand.Name = "logoBrand";
            this.logoBrand.Location = new Point(31, 19);
            this.logoBrand.Size = new Size(72, 72);

            // lblBrand: editable from Visual Studio Designer.
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Location = new Point(115, 24);
            this.lblBrand.Size = new Size(420, 43);
            this.lblBrand.AutoSize = false;
            this.lblBrand.BackColor = Color.Transparent;
            this.lblBrand.TabStop = false;
            this.lblBrand.Text = "Caro Online";
            this.lblBrand.Font = new Font("Segoe UI", 27F, FontStyle.Bold);
            this.lblBrand.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblBrand.TextAlign = ContentAlignment.MiddleLeft;

            // lblBrandSub: editable from Visual Studio Designer.
            this.lblBrandSub.Name = "lblBrandSub";
            this.lblBrandSub.Location = new Point(116, 66);
            this.lblBrandSub.Size = new Size(470, 28);
            this.lblBrandSub.AutoSize = false;
            this.lblBrandSub.BackColor = Color.Transparent;
            this.lblBrandSub.TabStop = false;
            this.lblBrandSub.Text = "Chơi Caro trực tuyến cùng bạn bè";
            this.lblBrandSub.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblBrandSub.ForeColor = Color.FromArgb(170, 168, 205);
            this.lblBrandSub.TextAlign = ContentAlignment.MiddleLeft;

            // profileChip: editable from Visual Studio Designer.
            this.profileChip.Name = "profileChip";
            this.profileChip.Location = new Point(830, 24);
            this.profileChip.Size = new Size(257, 66);
            this.profileChip.BackColor = Color.FromArgb(27, 27, 66);
            this.profileChip.GradientColor = Color.FromArgb(27, 27, 66);
            this.profileChip.BorderColor = Color.FromArgb(103, 91, 158);
            this.profileChip.CornerRadius = 16;

            // lblProfileDot: editable from Visual Studio Designer.
            this.lblProfileDot.Name = "lblProfileDot";
            this.lblProfileDot.Location = new Point(18, 18);
            this.lblProfileDot.Size = new Size(24, 29);
            this.lblProfileDot.AutoSize = false;
            this.lblProfileDot.BackColor = Color.Transparent;
            this.lblProfileDot.TabStop = false;
            this.lblProfileDot.Text = "●";
            this.lblProfileDot.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblProfileDot.ForeColor = Color.FromArgb(116, 236, 182);
            this.lblProfileDot.TextAlign = ContentAlignment.MiddleCenter;

            // lblProfileCaption: editable from Visual Studio Designer.
            this.lblProfileCaption.Name = "lblProfileCaption";
            this.lblProfileCaption.Location = new Point(54, 9);
            this.lblProfileCaption.Size = new Size(180, 22);
            this.lblProfileCaption.AutoSize = false;
            this.lblProfileCaption.BackColor = Color.Transparent;
            this.lblProfileCaption.TabStop = false;
            this.lblProfileCaption.Text = "XIN CHÀO,";
            this.lblProfileCaption.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            this.lblProfileCaption.ForeColor = Color.FromArgb(170, 168, 205);
            this.lblProfileCaption.TextAlign = ContentAlignment.MiddleLeft;

            // lblUsername: editable from Visual Studio Designer.
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Location = new Point(54, 30);
            this.lblUsername.Size = new Size(185, 28);
            this.lblUsername.AutoSize = false;
            this.lblUsername.BackColor = Color.Transparent;
            this.lblUsername.TabStop = false;
            this.lblUsername.Text = "demo";
            this.lblUsername.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblUsername.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblUsername.TextAlign = ContentAlignment.MiddleLeft;

            // lblGreeting: editable from Visual Studio Designer.
            this.lblGreeting.Name = "lblGreeting";
            this.lblGreeting.Location = new Point(33, 133);
            this.lblGreeting.Size = new Size(509, 49);
            this.lblGreeting.AutoSize = false;
            this.lblGreeting.BackColor = Color.Transparent;
            this.lblGreeting.TabStop = false;
            this.lblGreeting.Text = "Bắt đầu cuộc chơi";
            this.lblGreeting.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblGreeting.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblGreeting.TextAlign = ContentAlignment.MiddleLeft;

            // lblTopSub: editable from Visual Studio Designer.
            this.lblTopSub.Name = "lblTopSub";
            this.lblTopSub.Location = new Point(35, 183);
            this.lblTopSub.Size = new Size(490, 30);
            this.lblTopSub.AutoSize = false;
            this.lblTopSub.BackColor = Color.Transparent;
            this.lblTopSub.TabStop = false;
            this.lblTopSub.Text = "Chọn chế độ và thách đấu bạn bè ngay hôm nay.";
            this.lblTopSub.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblTopSub.ForeColor = Color.FromArgb(170, 168, 205);
            this.lblTopSub.TextAlign = ContentAlignment.MiddleLeft;

            // btnQuick: editable from Visual Studio Designer.
            this.btnQuick.Name = "btnQuick";
            this.btnQuick.Location = new Point(34, 239);
            this.btnQuick.Size = new Size(250, 112);
            this.btnQuick.ForeColor = Color.FromArgb(246, 244, 255);
            this.btnQuick.TabIndex = 0;
            this.btnQuick.Text = "▶   Chơi nhanh";
            this.btnQuick.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.btnQuick.CornerRadius = 12;
            this.btnQuick.BackColor = Color.FromArgb(49, 190, 235);
            this.btnQuick.GradientColor = Color.FromArgb(146, 86, 237);
            this.btnQuick.BorderColor = Color.FromArgb(173, 141, 255);

            // btnRooms: editable from Visual Studio Designer.
            this.btnRooms.Name = "btnRooms";
            this.btnRooms.Location = new Point(301, 239);
            this.btnRooms.Size = new Size(250, 112);
            this.btnRooms.ForeColor = Color.FromArgb(246, 244, 255);
            this.btnRooms.TabIndex = 1;
            this.btnRooms.Text = "Phòng chơi";
            this.btnRooms.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.btnRooms.CornerRadius = 12;
            this.btnRooms.BackColor = Color.FromArgb(49, 39, 93);
            this.btnRooms.GradientColor = Color.FromArgb(33, 29, 72);
            this.btnRooms.BorderColor = Color.FromArgb(101, 89, 158);

            // btnLeaderboard: editable from Visual Studio Designer.
            this.btnLeaderboard.Name = "btnLeaderboard";
            this.btnLeaderboard.Location = new Point(34, 370);
            this.btnLeaderboard.Size = new Size(250, 112);
            this.btnLeaderboard.ForeColor = Color.FromArgb(246, 244, 255);
            this.btnLeaderboard.TabIndex = 2;
            this.btnLeaderboard.Text = "★   Bảng xếp hạng";
            this.btnLeaderboard.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            this.btnLeaderboard.CornerRadius = 12;
            this.btnLeaderboard.BackColor = Color.FromArgb(49, 39, 93);
            this.btnLeaderboard.GradientColor = Color.FromArgb(33, 29, 72);
            this.btnLeaderboard.BorderColor = Color.FromArgb(101, 89, 158);

            // btnAccountSide: editable from Visual Studio Designer.
            this.btnAccountSide.Name = "btnAccountSide";
            this.btnAccountSide.Location = new Point(301, 370);
            this.btnAccountSide.Size = new Size(250, 112);
            this.btnAccountSide.ForeColor = Color.FromArgb(246, 244, 255);
            this.btnAccountSide.TabIndex = 3;
            this.btnAccountSide.Text = "Tài khoản";
            this.btnAccountSide.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.btnAccountSide.CornerRadius = 12;
            this.btnAccountSide.BackColor = Color.FromArgb(49, 39, 93);
            this.btnAccountSide.GradientColor = Color.FromArgb(33, 29, 72);
            this.btnAccountSide.BorderColor = Color.FromArgb(101, 89, 158);

            // infoPanel: editable from Visual Studio Designer.
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Location = new Point(34, 512);
            this.infoPanel.Size = new Size(518, 118);
            this.infoPanel.BackColor = Color.FromArgb(27, 27, 66);
            this.infoPanel.GradientColor = Color.FromArgb(27, 27, 66);
            this.infoPanel.BorderColor = Color.FromArgb(96, 85, 143);
            this.infoPanel.CornerRadius = 15;

            // lblInfoHeading: editable from Visual Studio Designer.
            this.lblInfoHeading.Name = "lblInfoHeading";
            this.lblInfoHeading.Location = new Point(22, 17);
            this.lblInfoHeading.Size = new Size(470, 26);
            this.lblInfoHeading.AutoSize = false;
            this.lblInfoHeading.BackColor = Color.Transparent;
            this.lblInfoHeading.TabStop = false;
            this.lblInfoHeading.Text = "THÁCH ĐẤU • KẾT NỐI • THĂNG HẠNG";
            this.lblInfoHeading.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblInfoHeading.ForeColor = Color.FromArgb(64, 217, 255);
            this.lblInfoHeading.TextAlign = ContentAlignment.MiddleLeft;

            // lblInfoBody: editable from Visual Studio Designer.
            this.lblInfoBody.Name = "lblInfoBody";
            this.lblInfoBody.Location = new Point(22, 45);
            this.lblInfoBody.Size = new Size(466, 52);
            this.lblInfoBody.AutoSize = false;
            this.lblInfoBody.BackColor = Color.Transparent;
            this.lblInfoBody.TabStop = false;
            this.lblInfoBody.Text = "Chơi cùng bạn bè, khám phá phòng chơi và rèn\nluyện chiến thuật với cộng đồng Caro Online.";
            this.lblInfoBody.Font = new Font("Segoe UI", 10.4F, FontStyle.Regular);
            this.lblInfoBody.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblInfoBody.TextAlign = ContentAlignment.MiddleLeft;

            // rankPanel: editable from Visual Studio Designer.
            this.rankPanel.Name = "rankPanel";
            this.rankPanel.Location = new Point(577, 133);
            this.rankPanel.Size = new Size(511, 232);
            this.rankPanel.BackColor = Color.FromArgb(27, 27, 66);
            this.rankPanel.GradientColor = Color.FromArgb(27, 27, 66);
            this.rankPanel.BorderColor = Color.FromArgb(96, 85, 143);
            this.rankPanel.CornerRadius = 17;

            // lblRankTitle: editable from Visual Studio Designer.
            this.lblRankTitle.Name = "lblRankTitle";
            this.lblRankTitle.Location = new Point(21, 17);
            this.lblRankTitle.Size = new Size(460, 31);
            this.lblRankTitle.AutoSize = false;
            this.lblRankTitle.BackColor = Color.Transparent;
            this.lblRankTitle.TabStop = false;
            this.lblRankTitle.Text = "★  Bảng xếp hạng";
            this.lblRankTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            this.lblRankTitle.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblRankTitle.TextAlign = ContentAlignment.MiddleLeft;

            // lblRankHeader: editable from Visual Studio Designer.
            this.lblRankHeader.Name = "lblRankHeader";
            this.lblRankHeader.Location = new Point(23, 58);
            this.lblRankHeader.Size = new Size(465, 25);
            this.lblRankHeader.AutoSize = false;
            this.lblRankHeader.BackColor = Color.Transparent;
            this.lblRankHeader.TabStop = false;
            this.lblRankHeader.Text = "TOP NGƯỜI CHƠI";
            this.lblRankHeader.Font = new Font("Segoe UI", 9.3F, FontStyle.Bold);
            this.lblRankHeader.ForeColor = Color.FromArgb(64, 217, 255);
            this.lblRankHeader.TextAlign = ContentAlignment.MiddleLeft;

            // lblRank1: editable from Visual Studio Designer.
            this.lblRank1.Name = "lblRank1";
            this.lblRank1.Location = new Point(27, 91);
            this.lblRank1.Size = new Size(327, 31);
            this.lblRank1.AutoSize = false;
            this.lblRank1.BackColor = Color.Transparent;
            this.lblRank1.TabStop = false;
            this.lblRank1.Text = "01     MinhKhoa";
            this.lblRank1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblRank1.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblRank1.TextAlign = ContentAlignment.MiddleLeft;

            // lblRank1Score: editable from Visual Studio Designer.
            this.lblRank1Score.Name = "lblRank1Score";
            this.lblRank1Score.Location = new Point(404, 91);
            this.lblRank1Score.Size = new Size(85, 31);
            this.lblRank1Score.AutoSize = false;
            this.lblRank1Score.BackColor = Color.Transparent;
            this.lblRank1Score.TabStop = false;
            this.lblRank1Score.Text = "1.560";
            this.lblRank1Score.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.lblRank1Score.ForeColor = Color.FromArgb(241, 142, 223);
            this.lblRank1Score.TextAlign = ContentAlignment.MiddleRight;

            // lblRank2: editable from Visual Studio Designer.
            this.lblRank2.Name = "lblRank2";
            this.lblRank2.Location = new Point(27, 130);
            this.lblRank2.Size = new Size(327, 31);
            this.lblRank2.AutoSize = false;
            this.lblRank2.BackColor = Color.Transparent;
            this.lblRank2.TabStop = false;
            this.lblRank2.Text = "02     ThanhHien";
            this.lblRank2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblRank2.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblRank2.TextAlign = ContentAlignment.MiddleLeft;

            // lblRank2Score: editable from Visual Studio Designer.
            this.lblRank2Score.Name = "lblRank2Score";
            this.lblRank2Score.Location = new Point(404, 130);
            this.lblRank2Score.Size = new Size(85, 31);
            this.lblRank2Score.AutoSize = false;
            this.lblRank2Score.BackColor = Color.Transparent;
            this.lblRank2Score.TabStop = false;
            this.lblRank2Score.Text = "1.480";
            this.lblRank2Score.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.lblRank2Score.ForeColor = Color.FromArgb(64, 217, 255);
            this.lblRank2Score.TextAlign = ContentAlignment.MiddleRight;

            // lblRank3: editable from Visual Studio Designer.
            this.lblRank3.Name = "lblRank3";
            this.lblRank3.Location = new Point(27, 169);
            this.lblRank3.Size = new Size(327, 31);
            this.lblRank3.AutoSize = false;
            this.lblRank3.BackColor = Color.Transparent;
            this.lblRank3.TabStop = false;
            this.lblRank3.Text = "03     BaoNam";
            this.lblRank3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblRank3.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblRank3.TextAlign = ContentAlignment.MiddleLeft;

            // lblRank3Score: editable from Visual Studio Designer.
            this.lblRank3Score.Name = "lblRank3Score";
            this.lblRank3Score.Location = new Point(404, 169);
            this.lblRank3Score.Size = new Size(85, 31);
            this.lblRank3Score.AutoSize = false;
            this.lblRank3Score.BackColor = Color.Transparent;
            this.lblRank3Score.TabStop = false;
            this.lblRank3Score.Text = "1.390";
            this.lblRank3Score.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            this.lblRank3Score.ForeColor = Color.FromArgb(170, 168, 205);
            this.lblRank3Score.TextAlign = ContentAlignment.MiddleRight;

            // roomPanel: editable from Visual Studio Designer.
            this.roomPanel.Name = "roomPanel";
            this.roomPanel.Location = new Point(577, 381);
            this.roomPanel.Size = new Size(511, 249);
            this.roomPanel.BackColor = Color.FromArgb(27, 27, 66);
            this.roomPanel.GradientColor = Color.FromArgb(27, 27, 66);
            this.roomPanel.BorderColor = Color.FromArgb(96, 85, 143);
            this.roomPanel.CornerRadius = 17;

            // lblRoomsTitle: editable from Visual Studio Designer.
            this.lblRoomsTitle.Name = "lblRoomsTitle";
            this.lblRoomsTitle.Location = new Point(22, 20);
            this.lblRoomsTitle.Size = new Size(464, 34);
            this.lblRoomsTitle.AutoSize = false;
            this.lblRoomsTitle.BackColor = Color.Transparent;
            this.lblRoomsTitle.TabStop = false;
            this.lblRoomsTitle.Text = "Phòng đang chờ (3)";
            this.lblRoomsTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            this.lblRoomsTitle.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblRoomsTitle.TextAlign = ContentAlignment.MiddleLeft;

            // lblRoomsHint: editable from Visual Studio Designer.
            this.lblRoomsHint.Name = "lblRoomsHint";
            this.lblRoomsHint.Location = new Point(24, 61);
            this.lblRoomsHint.Size = new Size(454, 28);
            this.lblRoomsHint.AutoSize = false;
            this.lblRoomsHint.BackColor = Color.Transparent;
            this.lblRoomsHint.TabStop = false;
            this.lblRoomsHint.Text = "Chọn phòng để bắt đầu kết nối";
            this.lblRoomsHint.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.lblRoomsHint.ForeColor = Color.FromArgb(170, 168, 205);
            this.lblRoomsHint.TextAlign = ContentAlignment.MiddleLeft;

            // lblRoom1: editable from Visual Studio Designer.
            this.lblRoom1.Name = "lblRoom1";
            this.lblRoom1.Location = new Point(24, 94);
            this.lblRoom1.Size = new Size(251, 31);
            this.lblRoom1.AutoSize = false;
            this.lblRoom1.BackColor = Color.Transparent;
            this.lblRoom1.TabStop = false;
            this.lblRoom1.Text = "Phòng #102";
            this.lblRoom1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblRoom1.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblRoom1.TextAlign = ContentAlignment.MiddleLeft;

            // lblCount1: editable from Visual Studio Designer.
            this.lblCount1.Name = "lblCount1";
            this.lblCount1.Location = new Point(301, 94);
            this.lblCount1.Size = new Size(59, 31);
            this.lblCount1.AutoSize = false;
            this.lblCount1.BackColor = Color.Transparent;
            this.lblCount1.TabStop = false;
            this.lblCount1.Text = "1/2";
            this.lblCount1.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblCount1.ForeColor = Color.FromArgb(64, 217, 255);
            this.lblCount1.TextAlign = ContentAlignment.MiddleCenter;

            // btnJoin1: editable from Visual Studio Designer.
            this.btnJoin1.Name = "btnJoin1";
            this.btnJoin1.Location = new Point(386, 94);
            this.btnJoin1.Size = new Size(96, 31);
            this.btnJoin1.ForeColor = Color.FromArgb(246, 244, 255);
            this.btnJoin1.TabIndex = 4;
            this.btnJoin1.Text = "Vào";
            this.btnJoin1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnJoin1.CornerRadius = 12;
            this.btnJoin1.BackColor = Color.FromArgb(49, 39, 93);
            this.btnJoin1.GradientColor = Color.FromArgb(33, 29, 72);
            this.btnJoin1.BorderColor = Color.FromArgb(101, 89, 158);

            // lblRoom2: editable from Visual Studio Designer.
            this.lblRoom2.Name = "lblRoom2";
            this.lblRoom2.Location = new Point(24, 139);
            this.lblRoom2.Size = new Size(251, 31);
            this.lblRoom2.AutoSize = false;
            this.lblRoom2.BackColor = Color.Transparent;
            this.lblRoom2.TabStop = false;
            this.lblRoom2.Text = "Phòng #107";
            this.lblRoom2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblRoom2.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblRoom2.TextAlign = ContentAlignment.MiddleLeft;

            // lblCount2: editable from Visual Studio Designer.
            this.lblCount2.Name = "lblCount2";
            this.lblCount2.Location = new Point(301, 139);
            this.lblCount2.Size = new Size(59, 31);
            this.lblCount2.AutoSize = false;
            this.lblCount2.BackColor = Color.Transparent;
            this.lblCount2.TabStop = false;
            this.lblCount2.Text = "1/2";
            this.lblCount2.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblCount2.ForeColor = Color.FromArgb(64, 217, 255);
            this.lblCount2.TextAlign = ContentAlignment.MiddleCenter;

            // btnJoin2: editable from Visual Studio Designer.
            this.btnJoin2.Name = "btnJoin2";
            this.btnJoin2.Location = new Point(386, 139);
            this.btnJoin2.Size = new Size(96, 31);
            this.btnJoin2.ForeColor = Color.FromArgb(246, 244, 255);
            this.btnJoin2.TabIndex = 5;
            this.btnJoin2.Text = "Vào";
            this.btnJoin2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnJoin2.CornerRadius = 12;
            this.btnJoin2.BackColor = Color.FromArgb(49, 39, 93);
            this.btnJoin2.GradientColor = Color.FromArgb(33, 29, 72);
            this.btnJoin2.BorderColor = Color.FromArgb(101, 89, 158);

            // lblRoom3: editable from Visual Studio Designer.
            this.lblRoom3.Name = "lblRoom3";
            this.lblRoom3.Location = new Point(24, 184);
            this.lblRoom3.Size = new Size(251, 31);
            this.lblRoom3.AutoSize = false;
            this.lblRoom3.BackColor = Color.Transparent;
            this.lblRoom3.TabStop = false;
            this.lblRoom3.Text = "Phòng #115";
            this.lblRoom3.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblRoom3.ForeColor = Color.FromArgb(246, 244, 255);
            this.lblRoom3.TextAlign = ContentAlignment.MiddleLeft;

            // lblCount3: editable from Visual Studio Designer.
            this.lblCount3.Name = "lblCount3";
            this.lblCount3.Location = new Point(301, 184);
            this.lblCount3.Size = new Size(59, 31);
            this.lblCount3.AutoSize = false;
            this.lblCount3.BackColor = Color.Transparent;
            this.lblCount3.TabStop = false;
            this.lblCount3.Text = "2/2";
            this.lblCount3.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.lblCount3.ForeColor = Color.FromArgb(64, 217, 255);
            this.lblCount3.TextAlign = ContentAlignment.MiddleCenter;

            // btnJoin3: editable from Visual Studio Designer.
            this.btnJoin3.Name = "btnJoin3";
            this.btnJoin3.Location = new Point(386, 184);
            this.btnJoin3.Size = new Size(96, 31);
            this.btnJoin3.ForeColor = Color.FromArgb(246, 244, 255);
            this.btnJoin3.TabIndex = 6;
            this.btnJoin3.Text = "Vào";
            this.btnJoin3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnJoin3.CornerRadius = 12;
            this.btnJoin3.BackColor = Color.FromArgb(49, 39, 93);
            this.btnJoin3.GradientColor = Color.FromArgb(33, 29, 72);
            this.btnJoin3.BorderColor = Color.FromArgb(101, 89, 158);

            // btnLogoutSide: editable from Visual Studio Designer.
            this.btnLogoutSide.Name = "btnLogoutSide";
            this.btnLogoutSide.Location = new Point(33, 644);
            this.btnLogoutSide.Size = new Size(178, 32);
            this.btnLogoutSide.ForeColor = Color.FromArgb(246, 244, 255);
            this.btnLogoutSide.TabIndex = 7;
            this.btnLogoutSide.Text = "↪  Đăng xuất";
            this.btnLogoutSide.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLogoutSide.CornerRadius = 12;
            this.btnLogoutSide.BackColor = Color.FromArgb(49, 39, 93);
            this.btnLogoutSide.GradientColor = Color.FromArgb(33, 29, 72);
            this.btnLogoutSide.BorderColor = Color.FromArgb(101, 89, 158);

            // lblBuild: editable from Visual Studio Designer.
            this.lblBuild.Name = "lblBuild";
            this.lblBuild.Location = new Point(680, 646);
            this.lblBuild.Size = new Size(407, 26);
            this.lblBuild.AutoSize = false;
            this.lblBuild.BackColor = Color.Transparent;
            this.lblBuild.TabStop = false;
            this.lblBuild.Text = "CARO ONLINE   /   DESIGNER EDITION V4";
            this.lblBuild.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblBuild.ForeColor = Color.FromArgb(170, 168, 205);
            this.lblBuild.TextAlign = ContentAlignment.MiddleRight;

            this.btnQuick.Click += this.btnQuick_Click;
            this.btnRooms.Click += this.btnRooms_Click;
            this.btnLeaderboard.Click += this.btnLeaderboard_Click;
            this.btnAccountSide.Click += this.btnAccount_Click;
            this.btnJoin1.Click += this.btnRooms_Click;
            this.btnJoin2.Click += this.btnRooms_Click;
            this.btnJoin3.Click += this.btnRooms_Click;
            this.btnLogoutSide.Click += this.btnLogout_Click;

            this.Controls.Add(this.logoBrand);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.lblBrandSub);
            this.Controls.Add(this.profileChip);
            this.profileChip.Controls.Add(this.lblProfileDot);
            this.profileChip.Controls.Add(this.lblProfileCaption);
            this.profileChip.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblGreeting);
            this.Controls.Add(this.lblTopSub);
            this.Controls.Add(this.btnQuick);
            this.Controls.Add(this.btnRooms);
            this.Controls.Add(this.btnLeaderboard);
            this.Controls.Add(this.btnAccountSide);
            this.Controls.Add(this.infoPanel);
            this.infoPanel.Controls.Add(this.lblInfoHeading);
            this.infoPanel.Controls.Add(this.lblInfoBody);
            this.Controls.Add(this.rankPanel);
            this.rankPanel.Controls.Add(this.lblRankTitle);
            this.rankPanel.Controls.Add(this.lblRankHeader);
            this.rankPanel.Controls.Add(this.lblRank1);
            this.rankPanel.Controls.Add(this.lblRank1Score);
            this.rankPanel.Controls.Add(this.lblRank2);
            this.rankPanel.Controls.Add(this.lblRank2Score);
            this.rankPanel.Controls.Add(this.lblRank3);
            this.rankPanel.Controls.Add(this.lblRank3Score);
            this.Controls.Add(this.roomPanel);
            this.roomPanel.Controls.Add(this.lblRoomsTitle);
            this.roomPanel.Controls.Add(this.lblRoomsHint);
            this.roomPanel.Controls.Add(this.lblRoom1);
            this.roomPanel.Controls.Add(this.lblCount1);
            this.roomPanel.Controls.Add(this.btnJoin1);
            this.roomPanel.Controls.Add(this.lblRoom2);
            this.roomPanel.Controls.Add(this.lblCount2);
            this.roomPanel.Controls.Add(this.btnJoin2);
            this.roomPanel.Controls.Add(this.lblRoom3);
            this.roomPanel.Controls.Add(this.lblCount3);
            this.roomPanel.Controls.Add(this.btnJoin3);
            this.Controls.Add(this.btnLogoutSide);
            this.Controls.Add(this.lblBuild);

            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = new Size(1120, 690);
            this.BackColor = Color.FromArgb(16, 16, 44);
            this.Text = "Caro Online  ·  Launcher";
            this.Font = new Font("Segoe UI", 10F);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
        }
    }
}
