//
//  Info.cs
//  JsonFactory
//
//  Created by Merong World on 06/04/2016.
//  Copyright (c) 2016 Merong World. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonFactory
{
    class Info
    {
        public int Id;

        public string Addr;
        public string ClCd;
        public string ClCdNm;
        public string DrTotCnt;
        public string EmdongNm;
        public string EstbDd;
        public string GdrCnt;
        public string HospUrl;
        public string IntnCnt;
        public string PostNo;
        public string ResdntCnt;
        public string SdrCnt;
        public string SgguCd;
        public string SgguCdNm;
        public string SidoCd;
        public string SidoCdNm;
        public string Telno;
        public string XPos;
        public string YPos;
        public string YadmNm;
        public string Ykiho;

        public string EmyDayTelNo1;
        public string EmyDayTelNo2;
        public string EmyDayYn;

        public string EmyNgtTelNo1;
        public string EmyNgtTelNo2;
        public string EmyNgtYn;

        public string LunchSat;
        public string LunchWeek;

        public string NoTrmtHoli;
        public string NoTrmtSun;

        public string ParkEtc;
        public string ParkQty;
        public string ParkXpnsYn;

        public string PlcNm;
        public string PlcDir;
        public string PlcDist;

        public string RcvSat;
        public string RevWeek;

        public string TrmtFriEnd;
        public string TrmtFriStart;
        public string TrmtMonEnd;
        public string TrmtMonStart;
        public string TrmtSatEnd;
        public string TrmtSatStart;
        public string TrmtThuEnd;
        public string TrmtThuStart;
        public string TrmtTueEnd;
        public string TrmtTueStart;
        public string TrmtWedEnd;
        public string TrmtWedStart;

        public List<Dept> DeptList;
    }

    class BasicInfo
    {
        public int Id;

        public string Addr;
        public string ClCd;
        public string ClCdNm;
        public string DrTotCnt;
        public string EmdongNm;
        public string EstbDd;
        public string GdrCnt;
        public string HospUrl;
        public string IntnCnt;
        public string PostNo;
        public string ResdntCnt;
        public string SdrCnt;
        public string SgguCd;
        public string SgguCdNm;
        public string SidoCd;
        public string SidoCdNm;
        public string Telno;
        public string XPos;
        public string YPos;
        public string YadmNm;
        public string Ykiho;
    }

    class DetailInfo
    {
        public int Id;

        public string EmyDayTelNo1;
        public string EmyDayTelNo2;
        public string EmyDayYn;

        public string EmyNgtTelNo1;
        public string EmyNgtTelNo2;
        public string EmyNgtYn;

        public string LunchSat;
        public string LunchWeek;

        public string NoTrmtHoli;
        public string NoTrmtSun;

        public string ParkEtc;
        public string ParkQty;
        public string ParkXpnsYn;

        public string PlcNm;
        public string PlcDir;
        public string PlcDist;

        public string RcvSat;
        public string RevWeek;

        public string TrmtFriEnd;
        public string TrmtFriStart;
        public string TrmtMonEnd;
        public string TrmtMonStart;
        public string TrmtSatEnd;
        public string TrmtSatStart;
        public string TrmtThuEnd;
        public string TrmtThuStart;
        public string TrmtTueEnd;
        public string TrmtTueStart;
        public string TrmtWedEnd;
        public string TrmtWedStart;
    }

    class DeptInfo
    {
        public int Id;
        public List<Dept> DeptList;
    }

    class Dept
    {
        public string CdiagDrCnt;
        public string DgsbjtCd;
        public string DgsbjtCdNm;
        public string DgsbjtPrSdrCnt;
    }
}
