//
//  JsonFactory.cs
//  JsonFactory
//
//  Created by Merong World on 06/04/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace JsonFactory
{
    class JsonFactory
    {
        // You MUST put your own authorization key.
        private const string _authKey = "Place the authorization key here.";

        private List<District> _districtList;

        // Initialize district list.
        private void InitDistrictList()
        {
            _districtList.Add(new District("jongro", 110016));
            _districtList.Add(new District("junggu", 110017));
            _districtList.Add(new District("yongsan", 110014));
            _districtList.Add(new District("seongdong", 110011));
            _districtList.Add(new District("gwangjin", 110023));
            _districtList.Add(new District("dongdaemun", 110007));
            _districtList.Add(new District("jungnang", 110019));
            _districtList.Add(new District("seongbuk", 110012));
            _districtList.Add(new District("gangbuk", 110024));
            _districtList.Add(new District("dobong", 110006));
            _districtList.Add(new District("nowon", 110022));
            _districtList.Add(new District("eunpyeong", 110015));
            _districtList.Add(new District("seodaemun", 110010));
            _districtList.Add(new District("mapo", 110009));
            _districtList.Add(new District("yangcheon", 110020));
            _districtList.Add(new District("gangseo", 110003));
            _districtList.Add(new District("guro", 110005));
            _districtList.Add(new District("geumcheon", 110025));
            _districtList.Add(new District("yeongdeungpo", 110013));
            _districtList.Add(new District("dongjak", 110008));
            _districtList.Add(new District("gwanak", 110004));
            _districtList.Add(new District("seocho", 110021));
            _districtList.Add(new District("gangnam", 110001));
            _districtList.Add(new District("songpa", 110018));
            _districtList.Add(new District("gangdong", 110002));
        }

        private Boolean GetInfo(District district)
        {
            List<Info> infoList = new List<Info>();

            string basicPrefix = "http://openapi.hira.or.kr/openapi/service/hospInfoService";
            string basicService = "getHospBasisList?";
            string detailPrefix = "http://openapi.hira.or.kr/openapi/service/medicInsttDetailInfoService";
            string detailService = "getDetailInfo?";
            string deptPrefix = "http://openapi.hira.or.kr/openapi/service/medicInsttDetailInfoService";
            string deptService = "getMdlrtSbjectInfoList?";

            // Get total hospital count from OpenAPI.
            int totalCount = 0;
            string basicUrl = basicPrefix + "/" + basicService + "&ServiceKey=" + _authKey;
            string parameters = "&numOfRows=1" + "&sidoCd=110000" + "&sgguCd=" + district.Code;
            string requestUrl = basicUrl + parameters;

            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(requestUrl);
            string response = new StreamReader(stream).ReadToEnd();

            XmlDocument document = new XmlDocument();
            document.LoadXml(response);
            XmlNodeList nodeList = document.GetElementsByTagName("body");

            foreach (XmlNode node in nodeList)
            {
                if (node["totalCount"] == null)
                    return false;

                totalCount = Int32.Parse(node["totalCount"].InnerText);
                break;
            }

            for (int i = 0; i < totalCount; i++)
            {
                basicUrl = basicPrefix + "/" + basicService + "&ServiceKey=" + _authKey;
                parameters = "&pageNo=" + (i + 1) + "&numOfRows=1" + "&sidoCd=110000" + "&sgguCd=" + district.Code;
                requestUrl = basicUrl + parameters;

                stream = webClient.OpenRead(requestUrl);
                response = new StreamReader(stream).ReadToEnd();

                document.LoadXml(response);
                nodeList = document.GetElementsByTagName("item");

                Info info = new Info();
                foreach (XmlNode node in nodeList)
                {
                    info.Addr = (node["addr"] != null) ? node["addr"].InnerText : "";
                    info.ClCd = (node["clCd"] != null) ? node["clCd"].InnerText : "";
                    info.ClCdNm = (node["clCdNm"] != null) ? node["clCdNm"].InnerText : "";
                    info.DrTotCnt = (node["drTotCnt"] != null) ? node["drTotCnt"].InnerText : "";
                    info.EmdongNm = (node["emdongNm"] != null) ? node["emdongNm"].InnerText : "";
                    info.EstbDd = (node["estbDd"] != null) ? node["estbDd"].InnerText : "";
                    info.GdrCnt = (node["gdrCnt"] != null) ? node["gdrCnt"].InnerText : "";
                    info.HospUrl = (node["hospUrl"] != null) ? node["hospUrl"].InnerText : "";
                    info.IntnCnt = (node["intnCnt"] != null) ? node["intnCnt"].InnerText : "";
                    info.PostNo = (node["postNo"] != null) ? node["postNo"].InnerText : "";
                    info.ResdntCnt = (node["resdntCnt"] != null) ? node["resdntCnt"].InnerText : "";
                    info.SdrCnt = (node["sdrCnt"] != null) ? node["sdrCnt"].InnerText : "";
                    info.SgguCd = (node["sgguCd"] != null) ? node["sgguCd"].InnerText : "";
                    info.SgguCdNm = (node["sgguCdNm"] != null) ? node["sgguCdNm"].InnerText : "";
                    info.SidoCd = (node["sidoCd"] != null) ? node["sidoCd"].InnerText : "";
                    info.SidoCdNm = (node["sidoCdNm"] != null) ? node["sidoCdNm"].InnerText : "";
                    info.Telno = (node["telno"] != null) ? node["telno"].InnerText : "";
                    info.XPos = (node["XPos"] != null) ? node["XPos"].InnerText : "";
                    info.YPos = (node["YPos"] != null) ? node["YPos"].InnerText : "";
                    info.YadmNm = (node["yadmNm"] != null) ? node["yadmNm"].InnerText : "";
                    info.Ykiho = (node["ykiho"] != null) ? node["ykiho"].InnerText : "";

                    break;
                }

                Console.WriteLine("{0}", info.YadmNm);

                // Get detailed info using Ykiho retreived from basic info.
                string detailUrl = detailPrefix + "/" + detailService + "&ServiceKey=" + _authKey;
                parameters = "&ykiho=" + info.Ykiho;
                requestUrl = detailUrl + parameters;

                webClient = new WebClient();
                stream = webClient.OpenRead(requestUrl);
                response = new StreamReader(stream).ReadToEnd();

                document = new XmlDocument();
                document.LoadXml(response);
                nodeList = document.GetElementsByTagName("body");
                if (nodeList == null)
                {
                    continue;
                }

                foreach (XmlNode node in nodeList)
                {
                    if (node["item"] == null)
                    {
                        info.EmyDayTelNo1 = "";
                        info.EmyDayTelNo2 = "";
                        info.EmyDayYn = "";

                        info.EmyNgtTelNo1 = "";
                        info.EmyNgtTelNo2 = "";
                        info.EmyNgtYn = "";

                        info.LunchSat = "";
                        info.LunchWeek = "";

                        info.NoTrmtHoli = "";
                        info.NoTrmtSun = "";

                        info.ParkEtc = "";
                        info.ParkQty = "";
                        info.ParkXpnsYn = "";

                        info.PlcNm = "";
                        info.PlcDir = "";
                        info.PlcDist = "";

                        info.RcvSat = "";
                        info.RevWeek = "";

                        info.TrmtFriEnd = "";
                        info.TrmtFriStart = "";
                        info.TrmtMonEnd = "";
                        info.TrmtMonStart = "";
                        info.TrmtSatEnd = "";
                        info.TrmtSatStart = "";
                        info.TrmtThuEnd = "";
                        info.TrmtThuStart = "";
                        info.TrmtTueEnd = "";
                        info.TrmtTueStart = "";
                        info.TrmtWedEnd = "";
                        info.TrmtWedStart = "";

                        break;
                    }

                    XmlNode item = node["item"];
                    info.EmyDayTelNo1 = (item["emyDayTelNo1"] != null) ? item["emyDayTelNo1"].InnerText : "";
                    info.EmyDayTelNo2 = (item["emyDayTelNo2"] != null) ? item["emyDayTelNo2"].InnerText : "";
                    info.EmyDayYn = (item["emyDayYn"] != null) ? item["emyDayYn"].InnerText : "";

                    info.EmyNgtTelNo1 = (item["emyNgtTelNo1"] != null) ? item["emyNgtTelNo1"].InnerText : "";
                    info.EmyNgtTelNo2 = (item["emyNgtTelNo2"] != null) ? item["emyNgtTelNo2"].InnerText : "";
                    info.EmyNgtYn = (item["emyNgtYn"] != null) ? item["emyNgtYn"].InnerText : "";

                    info.LunchSat = (item["lunchSat"] != null) ? item["lunchSat"].InnerText : "";
                    info.LunchWeek = (item["lunchWeek"] != null) ? item["lunchWeek"].InnerText : "";

                    info.NoTrmtHoli = (item["noTrmtHoli"] != null) ? item["noTrmtHoli"].InnerText : "";
                    info.NoTrmtSun = (item["noTrmtSun"] != null) ? item["noTrmtSun"].InnerText : "";

                    info.ParkEtc = (item["parkEtc"] != null) ? item["parkEtc"].InnerText : "";
                    info.ParkQty = (item["parkQty"] != null) ? item["parkQty"].InnerText : "";
                    info.ParkXpnsYn = (item["parkXpnsYn"] != null) ? item["parkXpnsYn"].InnerText : "";

                    info.PlcNm = (item["plcNm"] != null) ? item["plcNm"].InnerText : "";
                    info.PlcDir = (item["plcDir"] != null) ? item["plcDir"].InnerText : "";
                    info.PlcDist = (item["plcDist"] != null) ? item["plcDist"].InnerText : "";

                    info.RcvSat = (item["rcvSat"] != null) ? item["rcvSat"].InnerText : "";
                    info.RevWeek = (item["rcvWeek"] != null) ? item["rcvWeek"].InnerText : "";

                    info.TrmtFriEnd = (item["trmtFriEnd"] != null) ? item["trmtFriEnd"].InnerText : "";
                    info.TrmtFriStart = (item["trmtFriStart"] != null) ? item["trmtFriStart"].InnerText : "";
                    info.TrmtMonEnd = (item["trmtMonEnd"] != null) ? item["trmtMonEnd"].InnerText : "";
                    info.TrmtMonStart = (item["trmtMonStart"] != null) ? item["trmtMonStart"].InnerText : "";
                    info.TrmtSatEnd = (item["trmtSatEnd"] != null) ? item["trmtSatEnd"].InnerText : "";
                    info.TrmtSatStart = (item["trmtSatStart"] != null) ? item["trmtSatStart"].InnerText : "";
                    info.TrmtThuEnd = (item["trmtThuEnd"] != null) ? item["trmtThuEnd"].InnerText : "";
                    info.TrmtThuStart = (item["trmtThuStart"] != null) ? item["trmtThuStart"].InnerText : "";
                    info.TrmtTueEnd = (item["trmtTueEnd"] != null) ? item["trmtTueEnd"].InnerText : "";
                    info.TrmtTueStart = (item["trmtTueStart"] != null) ? item["trmtTueStart"].InnerText : "";
                    info.TrmtWedEnd = (item["trmtWedEnd"] != null) ? item["trmtWedEnd"].InnerText : "";
                    info.TrmtWedStart = (item["trmtWedStart"] != null) ? item["trmtWedStart"].InnerText : "";
                    break;
                }

                // Get department info using Ykiho retreived from basic info.
                basicUrl = deptPrefix + "/" + deptService + "&ServiceKey=" + _authKey;
                parameters = "&ykiho=" + info.Ykiho + "&numOfRows=100";
                requestUrl = basicUrl + parameters;

                webClient = new WebClient();
                stream = webClient.OpenRead(requestUrl);
                response = new StreamReader(stream).ReadToEnd();

                document = new XmlDocument();
                document.LoadXml(response);
                nodeList = document.GetElementsByTagName("item");

                info.DeptList = new List<Dept>();

                if (nodeList.Count != 0)
                {
                    foreach (XmlNode node in nodeList)
                    {
                        Dept dept = new Dept();
                        dept.CdiagDrCnt = (node["cdiagDrCnt"] != null) ? node["cdiagDrCnt"].InnerText : "";
                        dept.DgsbjtCd = (node["dgsbjtCd"] != null) ? node["dgsbjtCd"].InnerText : "";
                        dept.DgsbjtCdNm = (node["dgsbjtCdNm"] != null) ? node["dgsbjtCdNm"].InnerText : "";
                        dept.DgsbjtPrSdrCnt = (node["dgsbjtPrSdrCnt"] != null) ? node["dgsbjtPrSdrCnt"].InnerText : "";
                        info.DeptList.Add(dept);
                    }
                }
                
                Console.WriteLine("current index : {0}", i);

                info.Id = i + 1;
                infoList.Add(info);
            }

            string json = JsonConvert.SerializeObject(infoList);
            System.IO.File.WriteAllText(@"D:\Git\FindDoc\JsonFactory\JsonFactory\json\" + district.Name + ".json", json);
            return true;
        }

        // Get basic info and save parsed data.
        private Boolean GetBasicInfo(District district)
        {
            List<BasicInfo>  basicInfoList = new List<BasicInfo>();
                                                                                                 
            string basicPrefix = "http://openapi.hira.or.kr/openapi/service/hospInfoService";
            string basicService = "getHospBasisList?";

            // Get total result count.
            int totalCount = 0;
            string basicUrl = basicPrefix + "/" + basicService + "&ServiceKey=" + _authKey;
            string parameters = "&numOfRows=1" + "&sidoCd=110000" + "&sgguCd=" + district.Code;
            string requestUrl = basicUrl + parameters;

            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(requestUrl);
            string response = new StreamReader(stream).ReadToEnd();

            XmlDocument document = new XmlDocument();
            document.LoadXml(response);
            XmlNodeList nodeList = document.GetElementsByTagName("body");
       
            foreach (XmlNode node in nodeList)
            {
                if (node["totalCount"] == null)
                    return false;

                totalCount = Int32.Parse(node["totalCount"].InnerText);
                break;
            }

            for (int i = 0; i < totalCount; i++)
            {
                basicUrl = basicPrefix + "/" + basicService + "&ServiceKey=" + _authKey;
                parameters = "&pageNo=" + (i + 1) + "&numOfRows=1" + "&sidoCd=110000" + "&sgguCd=" + district.Code;
                requestUrl = basicUrl + parameters;

                stream = webClient.OpenRead(requestUrl);
                response = new StreamReader(stream).ReadToEnd();

                document.LoadXml(response);
                nodeList = document.GetElementsByTagName("item");

                BasicInfo basicInfo = new BasicInfo();
                foreach (XmlNode node in nodeList)
                {
                    basicInfo.Addr = (node["addr"] != null) ? node["addr"].InnerText : "";
                    basicInfo.ClCd = (node["clCd"] != null) ? node["clCd"].InnerText : "";
                    basicInfo.ClCdNm = (node["clCdNm"] != null) ? node["clCdNm"].InnerText : "";
                    basicInfo.DrTotCnt = (node["drTotCnt"] != null) ? node["drTotCnt"].InnerText : "";
                    basicInfo.EmdongNm = (node["emdongNm"] != null) ? node["emdongNm"].InnerText : "";
                    basicInfo.EstbDd = (node["estbDd"] != null) ? node["estbDd"].InnerText : "";
                    basicInfo.GdrCnt = (node["gdrCnt"] != null) ? node["gdrCnt"].InnerText : "";
                    basicInfo.HospUrl = (node["hospUrl"] != null) ? node["hospUrl"].InnerText : "";
                    basicInfo.IntnCnt = (node["intnCnt"] != null) ? node["intnCnt"].InnerText : "";
                    basicInfo.PostNo = (node["postNo"] != null) ? node["postNo"].InnerText : "";
                    basicInfo.ResdntCnt = (node["resdntCnt"] != null) ? node["resdntCnt"].InnerText : "";
                    basicInfo.SdrCnt = (node["sdrCnt"] != null) ? node["sdrCnt"].InnerText : "";
                    basicInfo.SgguCd = (node["sgguCd"] != null) ? node["sgguCd"].InnerText : "";
                    basicInfo.SgguCdNm = (node["sgguCdNm"] != null) ? node["sgguCdNm"].InnerText : "";
                    basicInfo.SidoCd = (node["sidoCd"] != null) ? node["sidoCd"].InnerText : "";
                    basicInfo.SidoCdNm = (node["sidoCdNm"] != null) ? node["sidoCdNm"].InnerText : "";
                    basicInfo.Telno = (node["telno"] != null) ? node["telno"].InnerText : "";
                    basicInfo.XPos = (node["XPos"] != null) ? node["XPos"].InnerText : "";
                    basicInfo.YPos = (node["YPos"] != null) ? node["YPos"].InnerText : "";
                    basicInfo.YadmNm = (node["yadmNm"] != null) ? node["yadmNm"].InnerText : "";
                    basicInfo.Ykiho = (node["ykiho"] != null) ? node["ykiho"].InnerText : "";

                    break;
                }

                Console.WriteLine("Current location : {0}", i);

                basicInfo.Id = i + 1;
                basicInfoList.Add(basicInfo);
            }

            string json = JsonConvert.SerializeObject(basicInfoList);
            System.IO.File.WriteAllText(@"D:\Git\FindDoc\JsonFactory\JsonFactory\json\" + district.Name + "Basic.json", json);
            return true;
        }

        // Get detailed info and save data to a file.
        private Boolean GetDetailInfo(District district)
        {
            int i = 0;
            string detailPrefix = "http://openapi.hira.or.kr/openapi/service/medicInsttDetailInfoService";
            string detailService = "getDetailInfo?";

            string json = System.IO.File.ReadAllText(@"D:\Git\FindDoc\JsonFactory\JsonFactory\json\" + district.Name + "Basic.json");

            List<BasicInfo> basicInfoList = new List<BasicInfo>();
            basicInfoList = JsonConvert.DeserializeObject<List<BasicInfo>>(json);

            List<DetailInfo> detailInfoList = new List<DetailInfo>();
            foreach (BasicInfo basicInfo in basicInfoList)
            {
                string detailUrl = detailPrefix + "/" + detailService + "&ServiceKey=" + _authKey;
                string parameters = "&ykiho=" + basicInfo.Ykiho;
                string requestUrl = detailUrl + parameters;

                WebClient webClient = new WebClient();
                Stream stream = webClient.OpenRead(requestUrl);
                string response = new StreamReader(stream).ReadToEnd();

                XmlDocument document = new XmlDocument();
                document.LoadXml(response);
                XmlNodeList nodeList = document.GetElementsByTagName("body");
                if (nodeList == null)
                {
                    continue;
                }

                DetailInfo detailInfo = new DetailInfo();
                foreach (XmlNode node in nodeList)
                {
                    if (node["item"] == null)
                    {
                        break;
                    }
                    
                    XmlNode item = node["item"];
                    detailInfo.EmyDayTelNo1 = (item["emyDayTelNo1"] != null) ? item["emyDayTelNo1"].InnerText : "";
                    detailInfo.EmyDayTelNo2 = (item["emyDayTelNo2"] != null) ? item["emyDayTelNo2"].InnerText : "";
                    detailInfo.EmyDayYn = (item["emyDayYn"] != null) ? item["emyDayYn"].InnerText : "";

                    detailInfo.EmyNgtTelNo1 = (item["emyNgtTelNo1"] != null) ? item["emyNgtTelNo1"].InnerText : "";
                    detailInfo.EmyNgtTelNo2 = (item["emyNgtTelNo2"] != null) ? item["emyNgtTelNo2"].InnerText : "";
                    detailInfo.EmyNgtYn = (item["emyNgtYn"] != null) ? item["emyNgtYn"].InnerText : "";

                    detailInfo.LunchSat = (item["lunchSat"] != null) ? item["lunchSat"].InnerText : "";
                    detailInfo.LunchWeek = (item["lunchWeek"] != null) ? item["lunchWeek"].InnerText : "";

                    detailInfo.NoTrmtHoli = (item["noTrmtHoli"] != null) ? item["noTrmtHoli"].InnerText : "";
                    detailInfo.NoTrmtSun = (item["noTrmtSun"] != null) ? item["noTrmtSun"].InnerText : "";

                    detailInfo.ParkEtc = (item["parkEtc"] != null) ? item["parkEtc"].InnerText : "";
                    detailInfo.ParkQty = (item["parkQty"] != null) ? item["parkQty"].InnerText : "";
                    detailInfo.ParkXpnsYn = (item["parkXpnsYn"] != null) ? item["parkXpnsYn"].InnerText : "";

                    detailInfo.PlcNm = (item["plcNm"] != null) ? item["plcNm"].InnerText : "";
                    detailInfo.PlcDir = (item["plcDir"] != null) ? item["plcDir"].InnerText : "";
                    detailInfo.PlcDist = (item["plcDist"] != null) ? item["plcDist"].InnerText : "";

                    detailInfo.RcvSat = (item["rcvSat"] != null) ? item["rcvSat"].InnerText : "";
                    detailInfo.RevWeek = (item["rcvWeek"] != null) ? item["rcvWeek"].InnerText : "";

                    detailInfo.TrmtFriEnd = (item["trmtFriEnd"] != null) ? item["trmtFriEnd"].InnerText : "";
                    detailInfo.TrmtFriStart = (item["trmtFriStart"] != null) ? item["trmtFriStart"].InnerText : "";
                    detailInfo.TrmtMonEnd = (item["trmtMonEnd"] != null) ? item["trmtMonEnd"].InnerText : "";
                    detailInfo.TrmtMonStart = (item["trmtMonStart"] != null) ? item["trmtMonStart"].InnerText : "";
                    detailInfo.TrmtSatEnd = (item["trmtSatEnd"] != null) ? item["trmtSatEnd"].InnerText : "";
                    detailInfo.TrmtSatStart = (item["trmtSatStart"] != null) ? item["trmtSatStart"].InnerText : "";
                    detailInfo.TrmtThuEnd = (item["trmtThuEnd"] != null) ? item["trmtThuEnd"].InnerText : "";
                    detailInfo.TrmtThuStart = (item["trmtThuStart"] != null) ? item["trmtThuStart"].InnerText : "";
                    detailInfo.TrmtTueEnd = (item["trmtTueEnd"] != null) ? item["trmtTueEnd"].InnerText : "";
                    detailInfo.TrmtTueStart = (item["trmtTueStart"] != null) ? item["trmtTueStart"].InnerText : "";
                    detailInfo.TrmtWedEnd = (item["trmtWedEnd"] != null) ? item["trmtWedEnd"].InnerText : "";
                    detailInfo.TrmtWedStart = (item["trmtWedStart"] != null) ? item["trmtWedStart"].InnerText : "";

                    detailInfo.Id = basicInfo.Id;
                    detailInfoList.Add(detailInfo);
                    break;
                }

                Console.WriteLine("Current location : {0}", i++);
            }

            json = JsonConvert.SerializeObject(detailInfoList);
            System.IO.File.WriteAllText(@"D:\Git\FindDoc\JsonFactory\JsonFactory\json\" + district.Name + "Detail.json", json);
            return true;
        }

        // Get department info and save data to a file.
        private Boolean GetDeptInfo(District district)
        {
            int i = 0;
            string deptPrefix = "http://openapi.hira.or.kr/openapi/service/medicInsttDetailInfoService";
            string deptService = "getMdlrtSbjectInfoList?";

            string json = System.IO.File.ReadAllText(@"D:\Git\FindDoc\JsonFactory\JsonFactory\json\" + district.Name + "Basic.json");

            List<BasicInfo> basicInfoList = new List<BasicInfo>();
            basicInfoList = JsonConvert.DeserializeObject<List<BasicInfo>>(json);

            List<DeptInfo> deptInfoList = new List<DeptInfo>();
            foreach (BasicInfo basicInfo in basicInfoList)
            {
                string basicUrl = deptPrefix + "/" + deptService + "&ServiceKey=" + _authKey;
                string parameters = "&ykiho=" + basicInfo.Ykiho + "&numOfRows=100";
                string requestUrl = basicUrl + parameters;
                //Console.WriteLine(requestUrl);

                WebClient webClient = new WebClient();
                Stream stream = webClient.OpenRead(requestUrl);
                string response = new StreamReader(stream).ReadToEnd();

                XmlDocument document = new XmlDocument();
                document.LoadXml(response);
                XmlNodeList nodeList = document.GetElementsByTagName("item");

                DeptInfo deptInfo = new DeptInfo();
                deptInfo.DeptList = new List<Dept>();

                if (nodeList.Count == 0)
                {
                    continue;
                }

                foreach (XmlNode node in nodeList)
                {
                    Dept dept = new Dept();
                    dept.CdiagDrCnt = (node["cdiagDrCnt"] != null) ? node["cdiagDrCnt"].InnerText : "";
                    dept.DgsbjtCd = (node["dgsbjtCd"] != null) ? node["dgsbjtCd"].InnerText : "";
                    dept.DgsbjtCdNm = (node["dgsbjtCdNm"] != null) ? node["dgsbjtCdNm"].InnerText : "";
                    dept.DgsbjtPrSdrCnt = (node["dgsbjtPrSdrCnt"] != null) ? node["dgsbjtPrSdrCnt"].InnerText : "";
                    deptInfo.DeptList.Add(dept);
                }

                Console.WriteLine("Current location : {0}", basicInfo.Id - 1);

                deptInfo.Id = basicInfo.Id;
                deptInfoList.Add(deptInfo);
            }

            json = JsonConvert.SerializeObject(deptInfoList);
            System.IO.File.WriteAllText(@"D:\Git\FindDoc\JsonFactory\JsonFactory\json\" + district.Name + "Dept.json", json);
            return true;
        }

        public void Run()
        {
            // Initialize district list
            _districtList = new List<District>();
            InitDistrictList();

            foreach (District district in _districtList)
            {
                GetInfo(district);
            }

            // Parse basic information
            /*
            foreach (District district in _districtList)
            {
                GetBasicInfo(district);
            }
            */

            // Parse detailed information
            /*
            foreach (District district in _districtList)
            {
                GetDetailInfo(district);
            }
            */

            // Parse department information
            /*
            foreach (District district in _districtList)
            {
                GetDeptInfo(district);
            }
            */
        }
    }
}
