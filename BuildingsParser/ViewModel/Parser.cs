using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using BuildingsParser.Models;
using HtmlAgilityPack;

namespace BuildingsParser.ViewModel
{
    public class Parser
    {
        public List<ParserItem> Pars()
        {
            List<ParserItem> Items = new List<ParserItem>();
            HtmlDocument docHtml = new HtmlWeb().Load("http://barcinohomes.com/poisk-obektov-po-karte");
            HtmlNodeCollection nodes = docHtml.DocumentNode.SelectNodes("//ul");
            HtmlNodeCollection lists = nodes[0].SelectNodes("//li");

            foreach (HtmlNode list in lists)
            {
                if (list.ChildNodes.Count > 3)
                {
                    HtmlNodeCollection divs = list.ChildNodes;
                    if (divs[3].GetAttributeValue("class", null) == "item")
                    {
                        HtmlNodeCollection divs2 = divs[3].ChildNodes;
                        HtmlNodeCollection content = divs2[3].ChildNodes;
                        //content[1].InnerText;
                        //content[3].InnerText;
                        HtmlNodeCollection table = content[5].ChildNodes;
                        HtmlNodeCollection table1 = table[1].ChildNodes;
                        HtmlNodeCollection table2 = table[3].ChildNodes;
                        HtmlNodeCollection table3 = table[5].ChildNodes;

                        //table1[1].InnerText

                        string splited1 = table1[1].InnerText.Replace("Регион: ", "");

                        //table1[3].InnerText

                        string[] splited2 = table1[3].InnerText.Split(' ');

                        //table1[5].InnerText

                        string splited3 = table1[5].InnerText.Replace("Мебель: ", "");

                        //table2[1].InnerText

                        string splited4 = table2[1].InnerText;

                        //table2[3].InnerText

                        string[] splited5 = table2[3].InnerText.Split(' ');

                        //table2[5].InnerText

                        string splited6 = table2[5].InnerText.Replace("Бассейн: ", "");

                        //table3[1].InnerText

                        string splited7 = table3[1].InnerText.Replace("№ лота: ", "");

                        //table3[3].InnerText

                        string splited8 = table3[3].InnerText.Replace("Количество спален: ", "");

                        //table3[5].InnerText

                        string splited9 = table3[5].InnerText.Replace("Стоимость: ", "").Replace(" ", "").Replace("€", "");

                        Items.Add(new ParserItem()
                        {
                            Name = content[1].InnerText,
                            Description = content[3].InnerText,
                            Region = splited1,
                            Area = Int32.Parse(splited2[1]),
                            Furniture = splited3,
                            District = splited4,
                            Terrace = splited5[1],
                            Pool = splited6,
                            LotNumber = splited7,
                            Bedrooms = Int32.Parse(splited8),
                            Price = Int32.Parse(splited9)
                        });
                    }
                }
            }
            return Items;
        }
    }
}