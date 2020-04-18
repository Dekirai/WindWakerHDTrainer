using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindWakerHD_Rewrite
{
    public class Coordinates
    {
        public static async Task<uint[]> GetBoatCoordinates(string Boatcoordinates)
        {
            List<uint> getcoordinates = new List<uint>();
            switch (Boatcoordinates)
            {
                case "Forsaken Fortress (A1)":
                    getcoordinates.Add(0xC8930FB8);
                    getcoordinates.Add(0xC8909DD7);
                    break;

                case "Four-Eye Reef (A2)":
                    getcoordinates.Add(0xC885CD72);
                    getcoordinates.Add(0xC85416A9);
                    break;

                case "Western Fairy Island (A3)":
                    getcoordinates.Add(0xC89AB0D7);
                    getcoordinates.Add(0xC7C0D03D);
                    break;

                case "Three-Eye Reef (A4)":
                    getcoordinates.Add(0xC8A044EE);
                    getcoordinates.Add(0x467CD0A5);
                    break;

                case "Needle Rock Isle (A5)":
                    getcoordinates.Add(0xC8858943);
                    getcoordinates.Add(0x47E41043);
                    break;

                case "Diamond Steppe Island (A6)":
                    getcoordinates.Add(0xC89261C2);
                    getcoordinates.Add(0x48482F4C);
                    break;

                case "Horseshoe Island (A7)":
                    getcoordinates.Add(0xC898B035);
                    getcoordinates.Add(0x489291EA);
                    break;

                case "Star Island (B1)":
                    getcoordinates.Add(0xC846F899);
                    getcoordinates.Add(0xC89AEE0A);
                    break;

                case "Mother & Child Isles (B2)":
                    getcoordinates.Add(0xC82FC9B3);
                    getcoordinates.Add(0xC842A146);
                    break;

                case "Rock Spire Isle (B3)":
                    getcoordinates.Add(0xC85B4C78);
                    getcoordinates.Add(0xC7E3C9E2);
                    break;

                case "Greatfish Isle (B4)":
                    getcoordinates.Add(0xC84FEC74);
                    getcoordinates.Add(0x44E1D437);
                    break;

                case "Isle of Steel (B5)":
                    getcoordinates.Add(0xC83175EB);
                    getcoordinates.Add(0x47AAD0B3);
                    break;

                case "Five-Eye Reef (B6)":
                    getcoordinates.Add(0xC85D2E60);
                    getcoordinates.Add(0x4838B031);
                    break;

                case "Outset Island (B7)":
                    getcoordinates.Add(0xC846EF58);
                    getcoordinates.Add(0x4896C198);
                    break;

                case "North Fairy Island (C1)":
                    getcoordinates.Add(0xC7A12B40);
                    getcoordinates.Add(0xC88C3F90);
                    break;

                case "Spectacle Island (C2)":
                    getcoordinates.Add(0xC7E6100B);
                    getcoordinates.Add(0xC831A8C3);
                    break;

                case "Tingle Island (C3)":
                    getcoordinates.Add(0xC7CB684B);
                    getcoordinates.Add(0xC79D415E);
                    break;

                case "Cyclops Reef (C4)":
                    getcoordinates.Add(0xC78B5F55);
                    getcoordinates.Add(0x46B22602);
                    break;

                case "Stone Watcher Island (C5)":
                    getcoordinates.Add(0xC7F38F47);
                    getcoordinates.Add(0x47C98F6B);
                    break;

                case "Shark Island (C6)":
                    getcoordinates.Add(0xC7CDC57B);
                    getcoordinates.Add(0x4856CDF3);
                    break;

                case "Headstone Island (C7)":
                    getcoordinates.Add(0xC79EA3D3);
                    getcoordinates.Add(0x489EDC71);
                    break;

                case "Gale Isle (D1)":
                    getcoordinates.Add(0x46971B08);
                    getcoordinates.Add(0xC89A7281);
                    break;

                case "Windfall Island (D2)":
                    getcoordinates.Add(0xC465F586);
                    getcoordinates.Add(0xC840A5CA);
                    break;

                case "Northerin Triangle Island (D3)":
                    getcoordinates.Add(0xC5576D8D);
                    getcoordinates.Add(0xC7C18815);
                    break;

                case "Six-Eye Reef (D4)":
                    getcoordinates.Add(0xC6B3BE9A);
                    getcoordinates.Add(0x460B2034);
                    break;

                case "Southern Triangle Isle (D5)":
                    getcoordinates.Add(0xC5D72C78);
                    getcoordinates.Add(0x47C4710E);
                    break;

                case "Southern Fairy Island (D6)":
                    getcoordinates.Add(0xC69C9329);
                    getcoordinates.Add(0x482B0E31);
                    break;

                case "Two-Eye Reef (D7)":
                    getcoordinates.Add(0x45314D7E);
                    getcoordinates.Add(0x4883C4E7);
                    break;

                case "Crescent Moon Island (E1)":
                    getcoordinates.Add(0x47CED913);
                    getcoordinates.Add(0xC8916B85);
                    break;

                case "Pawprint Isle (E2)":
                    getcoordinates.Add(0x479E9DD0);
                    getcoordinates.Add(0xC831DEAE);
                    break;

                case "Eastern Fairy Island (E3)":
                    getcoordinates.Add(0x47A3E3F4);
                    getcoordinates.Add(0xC7B870A4);
                    break;

                case "Tower of Gods (E4)":
                    getcoordinates.Add(0x47C34F10);
                    getcoordinates.Add(0x465AC49C);
                    break;

                case "Private Oasis (E5)":
                    getcoordinates.Add(0x47DFFD36);
                    getcoordinates.Add(0x47ED234B);
                    break;

                case "Ice Ring Isle (E6)":
                    getcoordinates.Add(0x47A9F81B);
                    getcoordinates.Add(0x484C08B5);
                    break;

                case "Angular Isles (E7)":
                    getcoordinates.Add(0x47BCA696);
                    getcoordinates.Add(0x489B84DC);
                    break;

                case "Seven-Star Isles (F1)":
                    getcoordinates.Add(0x4854E597);
                    getcoordinates.Add(0xC89CEF46);
                    break;

                case "Dragon Roost Island (F2)":
                    getcoordinates.Add(0x48386208);
                    getcoordinates.Add(0xC842B00D);
                    break;

                case "Fire Mountain (F3)":
                    getcoordinates.Add(0x482939E8);
                    getcoordinates.Add(0xC7E592FA);
                    break;

                case "Eastern Triangle Island (F4)":
                    getcoordinates.Add(0x483D18D7);
                    getcoordinates.Add(0x44F079AF);
                    break;

                case "Bomb Island (F5)":
                    getcoordinates.Add(0x48403036);
                    getcoordinates.Add(0x47B80E17);
                    break;

                case "Forest Haven (F6)":
                    getcoordinates.Add(0x4857EF59);
                    getcoordinates.Add(0x483A155A);
                    break;

                case "Boating Course (F7)":
                    getcoordinates.Add(0x482D1A17);
                    getcoordinates.Add(0x488AAE85);
                    break;

                case "Overlook Island (G1)":
                    getcoordinates.Add(0x488FBFB5);
                    getcoordinates.Add(0xC893D144);
                    break;

                case "Flight Control Platform (G2)":
                    getcoordinates.Add(0x4883F101);
                    getcoordinates.Add(0xC84C7993);
                    break;

                case "Star Belt Archipelago (G3)":
                    getcoordinates.Add(0x4882BD5A);
                    getcoordinates.Add(0xC7C39918);
                    break;

                case "Thorned Fairy Island (G4)":
                    getcoordinates.Add(0x48987BBA);
                    getcoordinates.Add(0x46BAC4A3);
                    break;

                case "Bird's Peak Rock (G5)":
                    getcoordinates.Add(0x4892B3D5);
                    getcoordinates.Add(0x47CDE990);
                    break;

                case "Cliff Plateu Isles (G6)":
                    getcoordinates.Add(0x48878373);
                    getcoordinates.Add(0x483203F8);
                    break;

                case "Five-Star Isles (G7)":
                    getcoordinates.Add(0x4897AF4F);
                    getcoordinates.Add(0x4888647F);
                    break;

                default: //default is Outset Island
                    getcoordinates.Add(0xC846EF58);
                    getcoordinates.Add(0x4896C198);
                    break;
            }

            return getcoordinates.ToArray();
        }
    }
}
