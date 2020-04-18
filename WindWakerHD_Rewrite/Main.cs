using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WindWakerHD_Rewrite.Properties;
using MadMilkman.Ini;
using System.Diagnostics;
using System.Globalization;

namespace WindWakerHD_Rewrite
{
    public partial class Main : Form
    {

        private const uint CodeHandlerStart = 0x01133000;
        private const uint CodeHandlerEnd = 0x01134300;
        private const uint CodeHandlerEnabled = 0x10014CFC;
        private TCPGecko Gecko;
        private bool connected;
        private static uint lastpositionx;
        private static uint lastpositiony;
        private static uint lastpositionz;
        private static uint lastfacing;
        public Main()
        {
            InitializeComponent();
            wiiuip.Text = Settings.Default.LatestIP;
            mapbox.SelectedIndex = 0;
            inventoryBox.SelectedIndex = 0;
            bottleitem.SelectedIndex = 0;
            itemspoofbox.SelectedIndex = 0;
            objectold.SelectedIndex = 0;
            objectnew.SelectedIndex = 0;
            boatcoordinatesList.SelectedIndex = 0;
            maxheartsBox.SelectedIndex = 0;
            maxmagicBox.SelectedIndex = 0;
        }

        private void wiiuconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (wiiuconnect.Tag == null)
                {
                    Gecko = new TCPGecko(wiiuip.Text, 7331);

                    connected = Gecko.Connect();

                    if (connected)
                    {
                        MessageBox.Show("Successfully connected!");
                        wiiuconnect.Tag = "connect";
                        wiiuconnect.Text = "Disconnect";
                        cheatTab.Enabled = true;
                    }
                }
                else
                {
                    wiiuconnect.Tag = null;
                    Gecko.Disconnect();
                    wiiuconnect.Text = "Connect";
                    cheatTab.Enabled = false;
                    readcoordinates.Checked = false;
                    readmap.Checked = false;
                    coordinatestimer.Stop();
                    maptimer.Stop();
                }
            }
            catch (ETCPGeckoException ex)
            {
                connected = false;
                MessageBox.Show(ex.Message);
            }
            catch (System.Net.Sockets.SocketException)
            {
                connected = false;
                MessageBox.Show("The IP is not valid.");
            }
        }

        private void SetCheats(ICollection<Geckocodes.Cheat> cheats)
        {
            #region codehandler
            Geckocodes getcheat = new Geckocodes();
            Gecko.poke32(CodeHandlerEnabled, 0x00000000);
            var clear = CodeHandlerStart;
            while (clear <= CodeHandlerEnd)
            {
                this.Gecko.poke32(clear, 0x0);
                clear += 0x4;
            }
            #endregion
            var codes = new List<uint>();

            if (cheats.Contains(Geckocodes.Cheat.Health))
                foreach (var entry in getcheat.GetInfHealth())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.MoonJump))
                foreach (var entry in getcheat.GetMoonJump())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.Magic))
                foreach (var entry in getcheat.GetInfMagic())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.Rupee))
                foreach (var entry in getcheat.GetInfRupees())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.Arrow))
                foreach (var entry in getcheat.GetInfArrows())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.Bomb))
                foreach (var entry in getcheat.GetInfBombs())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.Air))
                foreach (var entry in getcheat.GetInfAir())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.ForestWater))
                foreach (var entry in getcheat.GetInfForestWater())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.InfItems))
                foreach (var entry in getcheat.GetInfItems())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.SuperSwim))
                foreach (var entry in getcheat.GetSuperswim())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.SuperCrouch))
                foreach (var entry in getcheat.GetSupercrouch())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.CompletedMap))
                foreach (var entry in getcheat.GetCompletedMap())
                    codes.Add(entry);

            if (cheats.Contains(Geckocodes.Cheat.AllCharts))
                foreach (var entry in getcheat.GetAllTreasureCharts())
                    codes.Add(entry);


            var address = CodeHandlerStart;

            foreach (var code in codes)
            {
                Gecko.poke32(address, code);
                address += 0x4;
            }

            Gecko.poke32(CodeHandlerEnabled, 0x00000001);
        }

        private void setoptions_Click(object sender, EventArgs e)
        {
            var selected = new List<Geckocodes.Cheat>();

            if (infhealth.Checked == true)
                selected.Add(Geckocodes.Cheat.Health);

            if (moonjump.Checked == true)
                selected.Add(Geckocodes.Cheat.MoonJump);

            if (infmagic.Checked == true)
                selected.Add(Geckocodes.Cheat.Magic);

            if (infrupees.Checked == true)
                selected.Add(Geckocodes.Cheat.Rupee);

            if (infarrows.Checked == true)
                selected.Add(Geckocodes.Cheat.Arrow);

            if (infbombs.Checked == true)
                selected.Add(Geckocodes.Cheat.Bomb);

            if (infair.Checked == true)
                selected.Add(Geckocodes.Cheat.Air);

            if (infforestwatertime.Checked == true)
                selected.Add(Geckocodes.Cheat.ForestWater);

            if (infitems.Checked == true)
                selected.Add(Geckocodes.Cheat.InfItems);

            if (superswim.Checked == true)
                selected.Add(Geckocodes.Cheat.SuperSwim);

            if (supercrouch.Checked == true)
                selected.Add(Geckocodes.Cheat.SuperCrouch);

            if (completedmap.Checked == true)
                selected.Add(Geckocodes.Cheat.CompletedMap);

            if (alltreasurecharts.Checked == true)
                selected.Add(Geckocodes.Cheat.AllCharts);

            SetCheats(selected);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.LatestIP = wiiuip.Text;
            Settings.Default.Save();
        }

        private void backtomenu_Click(object sender, EventArgs e)
        {
            Gecko.poke08(0x1098F293, 0x01);
        }

        private async void loadmap_Click(object sender, EventArgs e)
        {
            var readmap = await Map.SetMap(mapbox.Text);
            Gecko.poke32(0x109763F0, 0x00000000); //Clearing Stage name to prevent Crash
            Gecko.poke32(0x109763F4, 0x00000000); //Clearing Stage name to prevent Crash
            Gecko.poke32(0x109763F0, readmap[0]);
            Gecko.poke32(0x109763F4, readmap[1]);
            Gecko.poke08(0x109763F9, Convert.ToByte(spawnid.Value)); //Room ID
            Gecko.poke08(0x109763FA, Convert.ToByte(roomid.Value)); //Spawn ID
            Gecko.poke08(0x109763FB, Convert.ToByte(layerid.Value)); //Layer ID
            Gecko.poke08(0x109763FC, 0x01); //Reload Stage
        }

        private async void additem_Click(object sender, EventArgs e)
        {
            var readitemaddress = await Inventory.GetItemAddress(inventoryBox.Text);
            var readitemvalue = await Inventory.GetItemValue(inventoryBox.Text);
            Gecko.poke08(readitemaddress[0], readitemvalue[0]);
            if (inventoryBox.Text == "Power Bracelets")
                Gecko.poke08(0x15073736, 0xFF);
        }

        private async void removeitem_Click(object sender, EventArgs e)
        {
            var readitemaddress = await Inventory.GetItemAddress(inventoryBox.Text);
            Gecko.poke08(readitemaddress[0], 0xFF);
            if (inventoryBox.Text == "Power Bracelets")
                Gecko.poke08(0x15073736, 0x00);
            if (inventoryBox.Text == "Hero's Charm")
                Gecko.poke08(0x15073738, 0x00);
        }

        private async void bottle1_Click(object sender, EventArgs e)
        {
            var readbottlevalue = await Inventory.GetItemValue(bottleitem.Text);
            Gecko.poke08(0x150736CA, readbottlevalue[0]);
        }

        private async void bottle2_Click(object sender, EventArgs e)
        {
            var readbottlevalue = await Inventory.GetItemValue(bottleitem.Text);
            Gecko.poke08(0x150736CB, readbottlevalue[0]);
        }

        private async void bottle3_Click(object sender, EventArgs e)
        {
            var readbottlevalue = await Inventory.GetItemValue(bottleitem.Text);
            Gecko.poke08(0x150736CC, readbottlevalue[0]);
        }

        private async void bottle4_Click(object sender, EventArgs e)
        {
            var readbottlevalue = await Inventory.GetItemValue(bottleitem.Text);
            Gecko.poke08(0x150736CD, readbottlevalue[0]);
        }

        private async void sloty_Click(object sender, EventArgs e)
        {
            var readitemvalue = await Inventory.GetItemValue(itemspoofbox.Text);
            Gecko.poke08(0x10976E6C, readitemvalue[0]);
        }

        private async void slotx_Click(object sender, EventArgs e)
        {
            var readitemvalue = await Inventory.GetItemValue(itemspoofbox.Text);
            Gecko.poke08(0x10976E6B, readitemvalue[0]);
        }

        private async void slotr_Click(object sender, EventArgs e)
        {
            var readitemvalue = await Inventory.GetItemValue(itemspoofbox.Text);
            Gecko.poke08(0x10976E6D, readitemvalue[0]);
        }

        private void readmap_CheckedChanged(object sender, EventArgs e)
        {
            if (readmap.Checked)
                maptimer.Start();
            else
                maptimer.Stop();
        }

        private async void maptimer_Tick(object sender, EventArgs e)
        {
            try
            {
                string mapname = "";
                string origin = Gecko.peek(0x109763F0).ToString("X");
                string hexValues = Regex.Replace(origin, ".{2}", "$0 ");
                string[] hexValuesSplit = hexValues.Split(' ');
                foreach (string hex in hexValuesSplit)
                {
                    if (string.IsNullOrWhiteSpace(hex)) continue;
                    int value = Convert.ToInt32(hex, 16);
                    mapname += Char.ConvertFromUtf32(value);
                }

                string mapname2 = "";
                string origin2 = Gecko.peek(0x109763F4).ToString("X");
                string hexValues2 = Regex.Replace(origin2, ".{2}", "$0 ");
                string[] hexValuesSplit2 = hexValues2.Split(' ');
                foreach (string hex in hexValuesSplit2)
                {
                    if (string.IsNullOrWhiteSpace(hex)) continue;
                    int value = Convert.ToInt32(hex, 16);
                    mapname2 += Char.ConvertFromUtf32(value);
                }

                uint roomid = Gecko.peek(0x109763F9) >> ((~0x109763F9 & 2) * 4) & 0xff;
                uint spawnid = Gecko.peek(0x109763F9) >> ((~0x109763F9 & 4) * 4) & 0xff;
                uint layerid = Gecko.peek(0x109763F9) >> ((~0x109763F9 & 0) * 4) & 0xff;
                printstage.Text = mapname + mapname2;
                printroomid.Text = roomid.ToString();
                printspawnid.Text = spawnid.ToString();
                printlayerid.Text = layerid.ToString();
                var readrealroom = await Map.GetRoomName(roomid.ToString());

                if (printstage.Text == "sea")
                    printroomid.Text += $" ({readrealroom[0]})";

                printstage.Text += $" ({await Map.GetRealName(printstage.Text)})";
            }
            catch
            {
                wiiuconnect.PerformClick();
                MessageBox.Show("Disconnected due to a connection loss");
            }
        }

        private void reloadstage_Click(object sender, EventArgs e)
        {
            Gecko.poke08(0x109763FC, 0x01);
        }

        private async void replaceobject_Click(object sender, EventArgs e)
        {
            var readobjectaddress = await Objects.GetObjectAddress(objectold.Text);
            var readobjectvalue = await Objects.GetObjectValue(objectnew.Text);
            Gecko.poke32(readobjectaddress[0], readobjectvalue[0]);
            Gecko.poke08(0x109763FC, 0x01); //Reload the stage
        }

        private async void boatteleport_Click(object sender, EventArgs e)
        {
            var readboatcoordinate = await Coordinates.GetBoatCoordinates(boatcoordinatesList.Text);
            Gecko.poke32(0x48723EC4, readboatcoordinate[0]);
            Gecko.poke32(0x48723ECC, readboatcoordinate[1]);
        }

        private void bombRadius_CheckedChanged(object sender, EventArgs e)
        {
            if (bombRadius.Checked)
                Gecko.poke32(0x1050CA50, 0x46000000);
            else
                Gecko.poke32(0x1050CA50, 0x43480000);
        }

        private void bowRadius_CheckedChanged(object sender, EventArgs e)
        {
            if (bowRadius.Checked)
                Gecko.poke32(0x10509634, 0x45F00000);
            else
                Gecko.poke32(0x10509634, 0x40A00000);
        }

        private void hookshotSpeed_CheckedChanged(object sender, EventArgs e)
        {
            if (hookshotSpeed.Checked)
                Gecko.poke32(0x105138C0, 0x42000000);
            else
                Gecko.poke32(0x105138C0, 0x40E00000);
        }

        private void hookshotRange_CheckedChanged(object sender, EventArgs e)
        {
            if (hookshotRange.Checked)
                Gecko.poke32(0x1051392C, 0x45800000);
            else
                Gecko.poke32(0x1051392C, 0x41700000);
        }

        private void bowSpeed_CheckedChanged(object sender, EventArgs e)
        {
            if (bowSpeed.Checked)
                Gecko.poke32(0x1050962C, 0x46000000);
            else
                Gecko.poke32(0x1050962C, 0x43480000);
        }

        private void boomerangRange_CheckedChanged(object sender, EventArgs e)
        {
            if (boomerangRange.Checked)
            {
                Gecko.poke32(0x1050D1A4, 0x467A0000);
                Gecko.poke32(0x1050D1A8, 0x467A0000);
            }

            else
            {
                Gecko.poke32(0x1050D1A4, 0x459C4000);
                Gecko.poke32(0x1050D1A8, 0x451C4000);
            }
        }

        private void boomerangThrow_CheckedChanged(object sender, EventArgs e)
        {
            if (boomerangThrow.Checked)
                Gecko.poke32(0x1050D1D0, 0x43700000);
            else
                Gecko.poke32(0x1050D1D0, 0x42700000);
        }

        private async void sethearts_Click(object sender, EventArgs e)
        {
            var gethearts = await Linktweaks.GetMaxHearts(maxheartsBox.Text);
            Gecko.poke08(0x15073681, gethearts[0]);
        }

        private async void setmagic_Click(object sender, EventArgs e)
        {
            var getmagic = await Linktweaks.GetMaxMagic(maxmagicBox.Text);
            Gecko.poke08(0x15073693, getmagic[0]);
        }

        private void setarrows_Click(object sender, EventArgs e)
        {
            Gecko.poke08(0x150736EF, Convert.ToByte(maxarrowsBox.Value));
        }

        private void setbombs_Click(object sender, EventArgs e)
        {
            Gecko.poke08(0x150736F0, Convert.ToByte(maxbombsBox.Value));
        }

        private void readcoordinates_CheckedChanged(object sender, EventArgs e)
        {
            if (readcoordinates.Checked)
                coordinatestimer.Start();
            else
                coordinatestimer.Stop();
        }

        private void coordinatestimer_Tick(object sender, EventArgs e)
        {
            try
            {
                uint speed = 0x00000000;
                speed = Gecko.peek(0x10989C76);
                byte[] linkxbytes = BitConverter.GetBytes(Gecko.peek(0x1096EF48));
                byte[] linkybytes = BitConverter.GetBytes(Gecko.peek(0x1096EF4C));
                byte[] linkzbytes = BitConverter.GetBytes(Gecko.peek(0x1096EF50));
                byte[] linkspeedbytes = BitConverter.GetBytes(Gecko.peek(speed + 0x00006938));
                byte[] boatxbytes = BitConverter.GetBytes(Gecko.peek(0x48723EC4));
                byte[] boatybytes = BitConverter.GetBytes(Gecko.peek(0x48723EC8));
                byte[] boatzbytes = BitConverter.GetBytes(Gecko.peek(0x48723ECC));
                float LinkX = BitConverter.ToSingle(linkxbytes, 0);
                float LinkY = BitConverter.ToSingle(linkybytes, 0);
                float LinkZ = BitConverter.ToSingle(linkzbytes, 0);
                float LinkSpeed = BitConverter.ToSingle(linkspeedbytes, 0);
                float BoatX = BitConverter.ToSingle(boatxbytes, 0);
                float BoatY = BitConverter.ToSingle(boatybytes, 0);
                float BoatZ = BitConverter.ToSingle(boatzbytes, 0);
                linkx.Text = LinkX.ToString();
                linky.Text = LinkY.ToString();
                linkz.Text = LinkZ.ToString();
                linkspeed.Text = LinkSpeed.ToString();
                boatx.Text = BoatX.ToString();
                boaty.Text = BoatY.ToString();
                boatz.Text = BoatZ.ToString();
            }
            catch
            {
                wiiuconnect.PerformClick();
                MessageBox.Show("Disconnected due to a connection loss");
            }
        }

        private void linkteleport_Click(object sender, EventArgs e)
        {
            uint collision = 0x00000000;
            uint collisionold = 0x00000000;
            collision = Gecko.peek(0x1097648C);
            collisionold = Gecko.peek(collision + 0x00000834);
            Gecko.poke32(collision + 0x00000834, 0x00004004);
            Gecko.poke32(0x1096EF48, lastpositionx);
            Gecko.poke32(0x1096EF4C, lastpositiony);
            Gecko.poke32(0x1096EF50, lastpositionz);
            Gecko.poke32(0x1096EF10, lastfacing);
            Thread.Sleep(1000);
            Gecko.poke32(collision + 0x00000834, collisionold);
        }

        private async void mapbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            readrealmap.Text = await Map.GetRealName(mapbox.Text);
        }

        private void storeposition_Click(object sender, EventArgs e)
        {
            lastpositionx = Gecko.peek(0x1096EF48);
            lastpositiony = Gecko.peek(0x1096EF4C);
            lastpositionz = Gecko.peek(0x1096EF50);
            lastfacing = Gecko.peek(0x1096EF10);
        }

        #region Quick access menu
        private void geckocodesMenu_Click(object sender, EventArgs e)
        {
            cheatTab.SelectedIndex = 0;
        }

        private void linktweaksMenu_Click(object sender, EventArgs e)
        {
            cheatTab.SelectedIndex = 1;
        }

        private void itemspoofMenu_Click(object sender, EventArgs e)
        {
            cheatTab.SelectedIndex = 2;
        }

        private void inventoryMenu_Click(object sender, EventArgs e)
        {
            cheatTab.SelectedIndex = 3;
        }

        private void bottleMenu_Click(object sender, EventArgs e)
        {
            cheatTab.SelectedIndex = 4;
        }

        private void itemmodsMenu_Click(object sender, EventArgs e)
        {
            cheatTab.SelectedIndex = 5;
        }

        private void coordinatesMenu_Click(object sender, EventArgs e)
        {
            cheatTab.SelectedIndex = 6;
        }

        private void customteleportMenu_Click(object sender, EventArgs e)
        {
            cheatTab.SelectedIndex = 7;
        }

        private void objectswapMenu_Click(object sender, EventArgs e)
        {
            cheatTab.SelectedIndex = 8;
        }

        private void stageloaderMenu_Click(object sender, EventArgs e)
        {
            cheatTab.SelectedIndex = 9;
        }
        #endregion

        private void copycoordinates_Click(object sender, EventArgs e)
        {
            Clipboard.SetText($"X={Gecko.peek(0x1096EF48)}\nY={Gecko.peek(0x1096EF4C)}\nZ={Gecko.peek(0x1096EF50)}\nFacing={Gecko.peek(0x1096EF10)}");
        }

        private void opencoordinatesfile_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Environment.CurrentDirectory + @"\Coordinates.ini");
            }
            catch
            {
                MessageBox.Show("Couldn't find 'Coordinates.ini'.\n\nMake sure the file is in the same folder as the Program.");
            }

        }

        private void loadcoordinates_Click(object sender, EventArgs e)
        {
            linkcoordinatesBox.Items.Clear();
            IniFile ini = new IniFile();
            try
            {
                ini.Load(Environment.CurrentDirectory + @"\Coordinates.ini");
                foreach (var key in ini.Sections)
                    linkcoordinatesBox.Items.Add(key.Name);
                linkcoordinatesBox.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("Couldn't find 'Coordinates.ini'.\n\nMake sure the file is in the same folder as the Program.");
            }
        }

        private void customlinkteleport_Click(object sender, EventArgs e)
        {
            UInt32 x = 0;
            UInt32 y = 0;
            UInt32 z = 0;
            UInt32 facing = 0;
            IniFile ini = new IniFile();
            ini.Load(Environment.CurrentDirectory + @"\Coordinates.ini");
            uint collision = 0x00000000;
            uint collisionold = 0x00000000;
            collision = Gecko.peek(0x1097648C);
            collisionold = Gecko.peek(collision + 0x00000834);
            UInt32.TryParse(ini.Sections[linkcoordinatesBox.Text].Keys["X"].Value, out x);
            UInt32.TryParse(ini.Sections[linkcoordinatesBox.Text].Keys["Y"].Value, out y);
            UInt32.TryParse(ini.Sections[linkcoordinatesBox.Text].Keys["Z"].Value, out z);
            UInt32.TryParse(ini.Sections[linkcoordinatesBox.Text].Keys["Facing"].Value, out facing);
            Gecko.poke32(collision + 0x00000834, 0x00004004);
            Gecko.poke32(0x1096EF48, x);
            Gecko.poke32(0x1096EF4C, y);
            Gecko.poke32(0x1096EF50, z);
            Gecko.poke32(0x1096EF10, facing);
            Thread.Sleep(1000);
            Gecko.poke32(collision + 0x00000834, collisionold);

        }

        private void setcurrenthearts_Click(object sender, EventArgs e)
        {
            Gecko.poke08(0x15073683, Convert.ToByte(currentheartsBox.Value));
        }

        private void allfigurines_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("You need atleast 1 figurine to access the rooms.\nContinue?", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Gecko.poke32(0x15073D24, 0xFFFFFFFF);
                Gecko.poke32(0x15073D28, 0xFFFFFF06);
                Gecko.poke32(0x15073D30, 0xFFFFFFFF);
                Gecko.poke08(0x15073D40, 0xFF);
                Gecko.poke16(0x15073D54, 0xFFFF);
            }
            else if (dialogResult == DialogResult.No)
            {
                //do nothing
            }

        }

        private void githubItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/Dekirai/WindWakerHDTrainer");
        }

        private void dekiraiItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gbatemp.net/members/393668/");
        }

        private void cosmocortneyItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gbatemp.net/members/327808/");
        }

        private void theword21Item_Click(object sender, EventArgs e)
        {
            Process.Start("https://gbatemp.net/members/350100/");
        }

        private void gamepilItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gbatemp.net/members/399819/");
        }

        private void pikaarcItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://gbatemp.net/members/396014/");
        }
    }
}