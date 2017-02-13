using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data.Common;
using System.Data;


namespace East_CSharp
{
    class PRB
    {
        #region members___
        //массивы с периодами и вероятностью для расчета
        public int[] periodsOfRepeating = new int[7];
        public double[] probabilityOfExceedance = new double[7];


        StreamWriter ///id5,res,im2,
            fla, wrng_out, ctl, grp, outFile, gst, fls, DEAGREGA, DEAGREGA_RS, DEAGREGA2;

        public static Random random = new Random(DateTime.Now.Millisecond * DateTime.Now.Minute);
        Random rnd = new Random();

        public bool m_ch_make_katalog = false;
        public int m_e_int_voz_ot, m_e_int_voz_do;
        public double m_e_int_mag_do, m_e_int_mag_ot;
        double[,] mass_gist_lat_lon = new double[NRP + 1, 2];
        
        public double m_e_tmax = 0.1;
        public int m_e_iter = 1;
        private int position_rdom = 0;
        private int li = 0;

        DateTime tim_st, tim_cur;
        OleDbConnection maindb;
        public bool m_main_base_ok;

        OleDbDataAdapter rprf;
        OleDbDataAdapter rs, ra;
        OleDbDataAdapter rdom, rlin, rsloi, rbz;

        DataTable rdomDT = new DataTable("rdomDT");
        DataTable rlinDT = new DataTable("rlinDT");
        DataTable rsloiDT = new DataTable("rsloiDT");
        

        long posi;//позиция прогресс бара
        int total;//общее количество значений в прогрессбаре
        int k_progress;
        int percents;//позиция прогресс бара, вторая реализация
        int emexit;
        int dom_kol;
        int lin_kol;
        int och_kol;
        int inet1;
        int NETKOL;

        //переменный, задающиеся из главной формы
        public int flag_9999 = 0;
        public int i_gst1, i_gst2 = 0, i_gst3;
        public int i_rz, i_az, i_bz, i_cz;
        public int DBcount = 0;
        public int fastCalc, LCAT, DEAG;
        public int typeOfGrunt;
        public int ncycl;


        int day;
        int hours;
        int min;
        int sec;

        const int NM1 = 200; //50
        const int NRP = 10000;//Максимальное количество точек в подседке
        const int IMM = 30;
        const int IPAR = 14;////////qqqqqqqqqqqq
        const int IMGS = 80;
        const double DI = 0.1;
        const int IM3 = 7;
        const int KMIS = 5;
        const int MOD = 1;

      
        String str;
        DateTime ct, ct_endwork;
        long NEQ, NGE;
        double RAD, RR, rbb, rbb55, rbb60, rbb65, rbb70, rbb75, rbb80, TMAX, TMAX2, DIPF, kszon;
        double[] I_dependency = new double[8];
        double RESI;
        long KDEP, IY;
        long M2, ITWO, IA, IC, MIC, kp, i_p; //, KP
        double s;
        int NCYCL;
        double S12, S14, S15, UG, CL, H0, PR, U;//,U[4]
        double[] SN = new double[4], Q = new double[4];
        long[] IS = new long[10], IS1 = new long[10], ISD = new long[11];
        long In, IGPM3, IORM3, L;
        long K0, INMA, KTMP, KBR, IGIP, IGPM2, ITEST, IORM2, NIZ;
        double DET, BRA, SQ0, ALBYW, H0U1, H0L1, DIP1, DIP2, SA0, GLBVI, BALL, SDEVI, SDEVI1;
        double SMAG, SL0, SW0, S1, S2, S3, CSA0, CO, DIP, P, P1, CDPF, CTDPF, RHS, H0U, H0L, SH, SLD;
        double C1, CS, FL1, PCOV, SRCAZ, PHI1, AL1, AZBRK, DIPBRK, S, RM, H0U2, H0L2, SDIP1;
        double RMIN, DMIN, FLZL, FAZ, R3M, FL0, R, IORM1;
        double[] DIPPLS = new double[8];
        double SZON, PNUM, PNUM1, EPS, SDPF, DPTHU, DPTHL, AKDIP, AKAZ, PRT1, PRT2, DPS1, DVS1, DPS2, DVS2;
        long KSTIC, IGIST, INMAX, JMAX, NVS, KOBH, KUMUL, KPNTR;
        double SPR1, SPR2, SPR3, SPR4, SPR5, SPR6, SPR7, VI, RMI, CRN, RBALL3, UI, DT;
        long IY0, NZ1, NZ, NEQS, NFAIL, NGEN, NFAI, IND, ITY, KMAG, ISBR, IGER, IFLT, ISM;
        double SDEVA, SDEVM, TPR, TPR1, AMWBAS, DX1, UX, DEVL, UY, DEVC, DX2;
        double[] ATTPAR = new double[6];
        double CROT, SROT, FCOR, XRC, YRC, CP, SP, SUM, SUMW, XS, YS, ZS;

        //тип подвижки
        int typeOfMovement;

        double CC, RA, RB, DR, SB, XX, YY, R3D, DENOM, DIDA, DIDMW, R35, R36, R37, R38, R39;
        double GI0, AN1, AN2, DL, DW, HB, PHIB, RNLB, FCORB, CBET, SBET, tipRazbrosa;
        double ALB, AWB, RNWB, CMAG, AIBAS, ALBYWB, DIST, CONTRIB, RSWITCH;
        double RQ1, RQ2, CMW1, CMW2, CLW1, CLW2, DISTMIN, RBAS, AMLHBAS, AMW, ML;
        double PI, PHI0, AL0, X1, X2, X3, Y1, Y2, Y3, X, Y, AZ0;
        double NLB, NWB, NL, NL2, NW, NW2;
        long IMW, LLOOP;
        int IYR, IMON, IDAY, iii;
        long KPIECE, KPIECE0, NETPNT=0;
        int INDC1, INDC2;
        double CMAG1, CMAG2;
        long KPCAT, KFAIL;

        double FIRSTMAG,FIRSTMAGRound;

        double[,] IGST = new double[NRP + 1, 7];
        //long[,] IGST = new long[NRP + 1, 7];
        double[,] DEAGREG = new double[10, 77001];
        double[,] DEAGREGRespSpectr = new double[100, 77001];
        double[,] POVTOR = new double[10, NRP]; //double[,] POVTOR = new double[10, 500000];
        long ideg, jdeg;
        long KMOD;
        long[] KPNT = new long[7];
        long[] NSEG = new long[IMM + 1], L0 = new long[IMM + 1];
        long[,] ISOS = new long[IMM + 1, 101];
        double[] XE = new double[3];
        double SEC0, SEC;//	  REAL*8 SEC0,SEC
        double[] XP = new double[NM1 + 1], YP = new double[NM1 + 1], XPU = new double[NM1 + 1], YPU = new double[NM1 + 1], BR = new double[NM1 + 1], BRU = new double[NM1 + 1], IGRFM = new double[IMM + 1];
        double[] MGNT = new double[IMM + 1], MGN = new double[IMM + 1];
        double[] OMLH = new double[21], OMW = new double[21];
        double[] GRF = new double[IMM + 1], GR = new double[IMM + 1], GR2 = new double[IMM + 1], GRFM = new double[IMM + 1], KOEFF = new double[IMM + 1];
        double THR, AZS, SAZ, THR1, DIPS, SDIP;
        double[,] RECS = new double[3, 4];
        double[] HA = new double[3], VH = new double[3];
        double[] XNET = new double[NRP + 1], YNET = new double[NRP + 1];
        double[] SPAR = new double[IPAR + 1];
        double[] RPAR = new double[8], ABC = new double[10];
        double[,] XYP = new double[4, NM1 + 1];
        double[] PRT = new double[NM1 + 1], CB = new double[IMM + 1], CE = new double[IMM + 1], FL = new double[IMM + 1];
        double[] SL = new double[IMM + 1], SW = new double[IMM + 1], ER = new double[3], ED = new double[4];
        double[,] XB = new double[IMM + 1, 3];
        double[] US1 = new double[4], US2 = new double[4], US3 = new double[4], US4 = new double[4];
        double[,] QS = new double[9, 4], PRPD = new double[5, 4], SNQ = new double[9, 4];
        double[] PMG = new double[7];
        double[] DST3 = new double[IM3 + 1], BSM3 = new double[IM3 + 1];
        double R3DMIN;
        double[] balldeagr1;
        double[] balldeagr2;
        double[] balldeagr3;
        double[] balldeagr4;
        double[] balldeagr5;
        double[] balldeagr6;
        double[] balldeagr7;
        double[] balldeagrx;
        double[] balldeagry;

        double[,] PGA_deagreg;
        double[,] SA_0_1_deagreg;
        String PrefixName, NAME_IMP_DAT, NAME2, NAME4, NAME5, NAMEDEAG_RS, NAMEDEAGDOMLIN, NAMEPROC;
        ///	char NAME[4],NAME1[11],NAME2[16],NAME3[16],NAME4[16];
        String NAME6, NAME_NET_GEG, NAMGRP, NAME_Warning, NAMCTL, NAMEA;
        ///    char NAME5[16],NAME6[16],NAME7[11],NAME8[16],NAMGRP[16],NAME9[16],NAMCTL[16];
        String PARFLN, MTOMW, IndEffect;//CHARACTER*12 PARFLN,MTOMW	 !CDATE,CTIME,

        ///    char PARFLN[12],MTOMW[12];//CHARACTER*12 PARFLN,MTOMW	 !CDATE,CTIME,
        //char[ , ] ZONE = new char[ 12, 5 ];//CHARACTER*12 ZONE*4
        String ZONE = "";
        ///char[ , ] PRBL = new char[ 50, 7 ];//CHARACTER*4 PRBL(50)
        string[] PRBL = new string[NRP];
        string applicationDir = "";

        public string NameOfCurrentCalculation="";

        double[,] DeargLineamDoman = new double[20000, 9];//Массив для деагрегации, запись ид даменов
        int iiIND = 0;//индекс массива деагрегации доменов

        Deaggregation currentDeag;


        //Массив с длительностями
        double[,,] DurrationMassive;

        
        double[] DurationInABCDfile = new double[7];//новый массив с длительностями в файл 

        //DurationInABCDfile
        int[] NIo = new int[7];
        double[,] A2 = new double[62, 62];
        double[,] A3 = new double[62, 62];
        double[,] A4 = new double[62, 8];

        //Сетка
        string netfilePath = "";
        double lat, lon;
        bool netIsFile;

        public string NetFilePath { set { netfilePath = value; } }
        public double Lat { set { lat = value; } }
        public double Lon { set { lon = value; } }
        public bool NetIsFile { set { netIsFile = value; } }


        int round_i;
        int round_j;

        NormalRandom normRand = new NormalRandom();

        #endregion



        public PRB(string SQLitePath)
        {
            applicationDir = Application.StartupPath;//System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetExecutingAssembly().GetModules()[ 0 ].FullyQualifiedName.ToString() );

            if(!applicationDir.EndsWith( @"\" ))
                applicationDir += @"\";

            //string SQLitePath = applicationDir + "Main.mdb";
            ///maindb = new SQLiteConnection(@"Data Source=" + SQLitePath + ";");
            maindb = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SQLitePath);
           // POVTOR_BALL = new StreamWriter("EAST_VI_VII_VIII_IX.txt", false, Encoding.GetEncoding(1251));
           // POVTOR_BALL.WriteLine("LAT\tLON\tVI\tVII\tVIII\tIX");
for(int i = 0; i < 500; i++)
            PRBL[ i ] = "EAST" + String.Format( "{0:00}",i + 1 );

            BSM3[ 0 ] = 0.0;
            BSM3[ 1 ] = 0.0;
            BSM3[ 2 ] = 4.0;
            BSM3[ 3 ] = 5.0;
            BSM3[ 4 ] = 6.0;
            BSM3[ 5 ] = 7.0;
            BSM3[ 6 ] = 8.0;
            BSM3[ 7 ] = 9.0;
            DST3[ 0 ] = 0.0;
            DST3[ 1 ] = 0.0;
            DST3[ 2 ] = 39.0;
            DST3[ 3 ] = 100.0;
            DST3[ 4 ] = 203.0;
            DST3[ 5 ] = 341.0;
            DST3[ 6 ] = 501.0;
            DST3[ 7 ] = 780.0;
            R3DMIN = 3.0;//min distance for grid step of subsources AND also
            //switches on saturation near to a point source
            KMOD = -1;
            //    PI=4.*atan(1.);
            //    RAD=180./PI;
            PI = 3.141593;
            RAD = 180.0 / PI;//57.295780;

            m_e_int_mag_ot = 1.0;
            m_e_int_mag_do = 9.0;
            m_e_int_voz_do = 1000000000;
            m_e_int_voz_ot = 0;
            DBcount = 0;

            DurrationMassive = new double[NRP, 62, 62];
        }

        public bool Close()
        {
            try
            {
                if(maindb.State != ConnectionState.Closed)
                    maindb.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }

        private bool OpenBase()
        {
            try
            {
                if(maindb.State == ConnectionState.Closed)
                    maindb.Open();

                maindb.Close();
                maindb.Open();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private DataTable FillTable(string commandText)//, ref OleDbDataAdapter da)
        {
            DataTable DT = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter();
            DBcount++;
            try
            {
                //maindb.Open();
                OleDbCommand cmd = new OleDbCommand( commandText,maindb );
                //maindb.Close();

                da.SelectCommand = cmd;
                da.Fill( DT );

                return DT;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private bool InsertCommand(string commandText)//, ref OleDbDataAdapter da)
        {
            DBcount++;
            try
            {
                //maindb.Open();
                OleDbCommand cmd = new OleDbCommand( commandText,maindb );
                cmd.ExecuteNonQuery();
                //maindb.Close();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public double fmin(ref double[] ff)
        {
            double x_j;
            long i;
            for(i = 0,x_j = ff[ 0 ]; i < 7; i++)
            {
                if(ff[ i ] < x_j)
                    x_j = ff[ i ];
            }
            return x_j;
        }

        ///----------------------- METHODS ------------------------

        public void OpenData(System.ComponentModel.BackgroundWorker worker,
        System.ComponentModel.DoWorkEventArgs e)
        {
            

            i_gst2 = 0;
            emexit = 0;
            PrefixName = applicationDir + "EAST_2016_";

            //NAME3 = PrefixName + "_B.TXT"; //id5
            //NAME5 = PrefixName + "_C.TXT"; //res
            //NAME8 = PrefixName + "_D.TXT"; //im2
            NAMEA = PrefixName + "_ABCD.TXT"; //fla

            try
            {
                if(File.Exists( NAMEA ))
                    File.Delete( NAMEA );

                ///id5 = new StreamWriter( NAME3,false,Encoding.GetEncoding( 1251 ) );//.Open(NAME3,CFile::modeReadWrite|CFile::typeText|CFile::modeCreate);
                ///res = new StreamWriter( NAME5,false,Encoding.GetEncoding( 1251 ) );//.Open(NAME5,CFile::modeReadWrite|CFile::typeText|CFile::modeCreate);
                ///im2 = new StreamWriter( NAME8,false,Encoding.GetEncoding( 1251 ) );//.Open(NAME8,CFile::modeReadWrite|CFile::typeText|CFile::modeCreate);
                fla = new StreamWriter( NAMEA,false,Encoding.GetEncoding( 1251 ) );//.Open(NAMEA,CFile::modeReadWrite|CFile::typeText|CFile::modeCreate);
            }
            catch(Exception ex)
            {
                MessageBox.Show( "ERROR: " + ex.Message );
                return;
            }

            NAME_IMP_DAT = PrefixName + "INP.DAT";
            NAME_NET_GEG = PrefixName + "NET.GEG";
            NAME_Warning = PrefixName + "WRNG_OUT.TXT";

            try
            {
                if(File.Exists( NAME_Warning ))
                    File.Delete( NAME_Warning );
                wrng_out = new StreamWriter( NAME_Warning,false,Encoding.GetEncoding( 1251 ) );//.Open(NAME9,CFile::modeWrite|CFile::typeText|CFile::modeCreate);
            }
            catch(Exception ex)
            {
                MessageBox.Show( "ERROR: " + ex.Message );
                return;
            }

            //ret=m_ch_make_katalog == true;//AfxMessageBox("Создавать каталог?",MB_YESNO);//IDYES

            //if(m_ch_make_katalog)
            //  LCAT = 1; //сохранять каталог
            //else
            //LCAT = 1;
                
            //DEAG = 0; //запускать деагрегацию 1 или 0
           


            KPIECE = 0;
            tim_st = DateTime.Now;
            rdomDT = new DataTable( "rdomDT" );
            rlinDT = new DataTable( "rlinDT" );
            rsloiDT = new DataTable( "rsloiDT" );

            OpenBase();

            NameOfCurrentCalculation = "Начало расчета";
            worker.ReportProgress(0, NameOfCurrentCalculation);

            ClearZony();
            PreWork(worker,e);
            FillOtherTables();///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Close();
        }

        private void ClearZony()
        {
            InsertCommand( "delete * from Зоны_ВОЗ" );
        }

        private void PreWork(System.ComponentModel.BackgroundWorker worker,
        System.ComponentModel.DoWorkEventArgs e)
        {
            

            //CStringFile net,mto;
            StreamReader net = null, mto = null, BALLDEAGREG = null;

            //Назначаем всем одну БД
            //rs.m_pDatabase=ra.m_pDatabase=rbz.m_pDatabase=rsloi.m_pDatabase=&maindb;

            DataTable rsDT = new DataTable( "rsDT" );
            DataTable raDT = new DataTable( "raDT" );

            rs = new OleDbDataAdapter();
            ra = new OleDbDataAdapter();
            rdom = new OleDbDataAdapter();
            rlin = new OleDbDataAdapter();
            rbz = new OleDbDataAdapter();
            rsloi = new OleDbDataAdapter();//SQLiteDataAdapter( "", maindb );
            rprf = new OleDbDataAdapter();


            ITWO = 2;
            M2 = 0;
            int kk,k1,ii,NN = 0;
            long k22 = 0;
a307:
            rsDT = FillTable("select * from _Входные_параметры");
            //rs.Fill( rsDT );

            for (int i = 0; i < rsDT.Rows.Count; i++)
            {
                IYR = Convert.ToInt32(rsDT.Rows[0]["iyr"]);
                IMON = Convert.ToInt32(rsDT.Rows[0]["imon"]);
                IDAY = Convert.ToInt32(rsDT.Rows[0]["iday"]);
                MTOMW = Convert.ToString(rsDT.Rows[0]["mtomw"]);
                CMW1 = Convert.ToDouble(rsDT.Rows[0]["mw1"]);
                CMW2 = Convert.ToDouble(rsDT.Rows[0]["mw2"]);
                CLW1 = Convert.ToDouble(rsDT.Rows[0]["c1"]);
                CLW2 = Convert.ToDouble(rsDT.Rows[0]["c2"]);
                SDEVA = Convert.ToDouble(rsDT.Rows[0]["sdeva"]);
                SDEVM = Convert.ToDouble(rsDT.Rows[0]["sdevm"]);
                SDEVI = Convert.ToDouble(rsDT.Rows[0]["sdevi"]);
                //IY0 = Convert.ToInt64(rsDT.Rows[0]["iy"]);
                //задания рандомного значения
                IY0 = DateTime.Now.Millisecond * DateTime.Now.Minute;
                PARFLN = Convert.ToString(rsDT.Rows[0]["parfln"]);
                TPR = Convert.ToDouble(rsDT.Rows[0]["t"]);
                //NCYCL = Convert.ToInt32(rsDT.Rows[0]["ncycl"]);
                NCYCL = ncycl;
                rbb55 = Convert.ToDouble(rsDT.Rows[0]["rb55"]);
                rbb60 = Convert.ToDouble(rsDT.Rows[0]["rb60"]);
                rbb65 = Convert.ToDouble(rsDT.Rows[0]["rb65"]); 
                rbb70 = Convert.ToDouble(rsDT.Rows[0]["rb70"]); 
                rbb75 = Convert.ToDouble(rsDT.Rows[0]["rb75"]); 
                rbb80 = Convert.ToDouble(rsDT.Rows[0]["rb80"]);

                I_dependency[0] = Convert.ToDouble(rsDT.Rows[0]["I_2"]);
                I_dependency[1] = Convert.ToDouble(rsDT.Rows[0]["I_3"]);
                I_dependency[2] = Convert.ToDouble(rsDT.Rows[0]["I_4"]);
                I_dependency[3] = Convert.ToDouble(rsDT.Rows[0]["I_5"]);
                I_dependency[4] = Convert.ToDouble(rsDT.Rows[0]["I_6"]);
                I_dependency[5] = Convert.ToDouble(rsDT.Rows[0]["I_7"]);
                I_dependency[6] = Convert.ToDouble(rsDT.Rows[0]["I_8"]);
                I_dependency[7] = Convert.ToDouble(rsDT.Rows[0]["I_9"]);
            }


            rsDT.Clear();

            if (LCAT == 1)//&&(catinp.DoModal()!=2))
            {
                KPIECE0 = 1;
                INDC1 = m_e_int_voz_ot;
                INDC2 = m_e_int_voz_do;
                CMAG1 = m_e_int_mag_ot;
                CMAG2 = m_e_int_mag_do;
            }             
            
            
            KPCAT = 1;
            KPIECE = KPIECE + 1; //номер новой текущей подсетки
            if(KPIECE > 500) { EmExit( "Число сеток д.б. не больше 50" ); goto a306; };
            if(KPIECE == KPIECE0)//проверка на подсетку для сохранения каталога
            {
                if(LCAT == 1) //если надо сохранять каталог для данной подсетки готовим файл
                {////////////////////////////////////////////////////////////////////////////////////////
                    ///NAMCTL=pg3->m_e_make_katalog_name+PRBL[KPIECE-1]+"_CTL.TXT";
                    NAMCTL = "CTL.TXT";

                    try
                    {
                        //ctl.Open(NAMCTL,CFile::modeWrite|CFile::typeText|CFile::modeCreate);
                        ctl = new StreamWriter( applicationDir + NAMCTL,false,Encoding.GetEncoding( 1251 ) );
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show( "ERROR: " + ex.Message );
                        EmExit( "Невозможно создать выходной файл " + NAMCTL );
                        goto a306;
                    }
                    str = "INDZ\tMAG\tL\tW\tAZ\tDIP\tPHI1\tLMD1\tH1";
                    ctl.WriteLine(str);

                }
                KPCAT = 2;// флаг о том что подготовлен файл и надо сохранять каталог в этом цикле 
            }

            NAME6 = PrefixName + PRBL[ KPIECE - 1 ] + "_FLS.TXT";
            NAMGRP = PrefixName + PRBL[ KPIECE - 1 ] + "_GRP.TXT";
            KFAIL = 0;
            XNET = new double[NRP + 1];
            YNET = new double[NRP + 1];

            if (KPIECE == 1) //если сетка первая
            {

                if (netIsFile)
                {
                    try
                    {
                        //net.Open("mynet.geg");
                        net = new StreamReader(netfilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: " + ex.Message);
                        EmExit("Невозможно прочитать выходной файл 'mynet.geg'");
                        goto a306;
                    }
                }
                else
                {

                }
            }

            //rs.SelectCommand = new OleDbCommand("select * from _Входные_параметры");
            
            try
            {
                ct = new DateTime( IYR,IMON,IDAY );
            }
            catch
            {
                ct = DateTime.Now;
            }

            string aa = "";
            string aaa = "";

            AZ0 = 0.0;
a308:

              
            ii = 1;
            aa = "";


            if (!netIsFile)
            {
                if (NETPNT == 1)
                    goto a306;

                XNET[1] = lat;
                YNET[1] = lon;

                ii = 2;
            }
            else
            {
                while (net.EndOfStream != true)
                {
                    aa = net.ReadLine();

                int SEP1 = aa.IndexOf(" ");
                int SEP2 = aa.IndexOf("\t");
                string[] stringSeparators = new string[1] { " " };
                if (SEP1 >= 0)
                    stringSeparators[0] = " ";
                if (SEP2 >= 0)
                    stringSeparators[0] = "\t";

                String[] nums = aa.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                XNET[ii] = Convert.ToDouble(nums[0]);
                YNET[ii] = Convert.ToDouble(nums[1]);

                //XNET[ii] = Convert.ToDouble(aa.Substring(0, aa.IndexOf(" ")).Trim());
                //YNET[ii] = Convert.ToDouble(aa.Substring(aa.IndexOf(" ")).Trim());

                if (aa.StartsWith("9999.0"))
                    goto a305;
                if (aa.StartsWith("-9999.0"))
                    goto a306;

                ii++;
                if (ii > NRP)
                    {
                        MessageBox.Show("В подсетке " + Convert.ToString(KPIECE) + " превышено количество точек ( " + Convert.ToString(NRP) + " ), часть будет пропущена");
                        goto a305;
                    }
                }
            }



a305:

            /*
            while (net.EndOfStream != true)
            {
                aa = net.ReadLine();

                XNET[ii] = Convert.ToDouble(aa.Substring(0, aa.IndexOf(" ")).Trim());
                YNET[ii] = Convert.ToDouble(aa.Substring(aa.IndexOf(" ")).Trim());

                if (aa.StartsWith("9999.0"))
                    goto a305;
                if (aa.StartsWith("-9999.0"))
                    goto a306;

                ii++;
                if (ii > NRP)
                    goto a305;
            }
 */
            /* if (DEAG == 1)
             {
                 NETKOL = ii - 1;
                 for (int inet1 = 1; inet1 <= NETKOL; inet1++)
                 {
                     for (int ideg = 1; ideg < 12; ideg++)
                     {
                         for (int jdeg = 1; jdeg < 71; jdeg++)
                         {
                             DEAGREG[1, (ideg - 1) * 70 + jdeg + (inet1 - 1) * 770] = ((double)(ideg)) / 2 + 2.5;
                             DEAGREG[2, (ideg - 1) * 70 + jdeg + (inet1 - 1) * 770] = (double)(jdeg * 5);

                             DEAGREGRespSpectr[1, (ideg - 1) * 70 + jdeg + (inet1 - 1) * 770] = ((double)(ideg)) / 2 + 2.5;
                             DEAGREGRespSpectr[2, (ideg - 1) * 70 + jdeg + (inet1 - 1) * 770] = (double)(jdeg * 5);
                         }
                     }
                 }
             }*/

            NETPNT = ii - 1;//количество точек сетки

            if (DEAG == 1)//Проводить деагрегацию
            {
                BALLDEAGREG = new StreamReader(applicationDir + "EAST_2016__ABCD_.TXT");
                
            }
            iii = 1;
            aaa = "";
            if (DEAG == 1)
            {
                aaa = BALLDEAGREG.ReadLine();

                 balldeagrx = new double[NETPNT + 1];
                  balldeagry = new double[NETPNT + 1];
                  balldeagr1 = new double[NETPNT + 1];
                  balldeagr2 = new double[NETPNT + 1];
                 balldeagr3 = new double[NETPNT + 1];
                  balldeagr4 = new double[NETPNT + 1];
                   balldeagr5 = new double[NETPNT + 1];
                   balldeagr6 = new double[NETPNT + 1];
                   balldeagr7 = new double[NETPNT + 1];

                  PGA_deagreg = new double[NETPNT + 1, 7];
                  SA_0_1_deagreg = new double[NETPNT + 1, 7];

                while (BALLDEAGREG.EndOfStream != true)
                {
                    aaa = BALLDEAGREG.ReadLine();
                    String[] nums = aaa.Split('\t');
                    /*заполнение массивов значениями из файла*/
                    balldeagrx[iii] = Convert.ToDouble(nums[0]);
                    balldeagry[iii] = Convert.ToDouble(nums[1]);
                    balldeagr1[iii] = Convert.ToDouble(nums[2]);
                    balldeagr2[iii] = Convert.ToDouble(nums[3]);
                    balldeagr3[iii] = Convert.ToDouble(nums[4]);
                    balldeagr4[iii] = Convert.ToDouble(nums[5]);
                    balldeagr5[iii] = Convert.ToDouble(nums[6]);
                    balldeagr6[iii] = Convert.ToDouble(nums[7]);
                    balldeagr7[iii] = Convert.ToDouble(nums[8]);

                    //входные значения для PGA
                    for (int i = 0; i < 7; i++)
                    {
                        PGA_deagreg[iii, i] = Convert.ToDouble(nums[13 + (i * 11)]);
                        SA_0_1_deagreg[iii, i] = Convert.ToDouble(nums[14 + (i * 11)]);
                    }
                    
                    iii++;
                }

                BALLDEAGREG.Close();
                BALLDEAGREG.Dispose();

            }


            
            if(NETPNT == 0) { goto a308; }
            PHI0 = 0.0;
            AL0 = 0.0;

            if(DEAG == 1)
            {
                currentDeag = new Deaggregation(applicationDir + "EAST_2016__ABCD_.TXT", NETPNT);
           }
            

            for (ii = 1; ii <= NETPNT; ii++)
            {
                PHI0 = PHI0 + XNET[ ii ];
                AL0 = AL0 + YNET[ ii ];
            }


            a401:
            PHI0 = PHI0 / NETPNT;
            AL0 = AL0 / NETPNT;
            try
            {
                grp = new StreamWriter( NAMGRP,false,Encoding.GetEncoding( 1251 ) );
            }
            catch(Exception ex)
            {
                MessageBox.Show( "ERROR: " + ex.Message );
                EmExit( "Невозможно создать выходной файл " + NAMGRP );
                goto a306;
            }

            NAME2 = PrefixName + PRBL[ KPIECE - 1 ] + "_OUT.TXT";

            try
            {
                outFile = new StreamWriter( NAME2,false,Encoding.GetEncoding( 1251 ) );
            }
            catch(Exception ex)
            {
                MessageBox.Show( "ERROR: " + ex.Message );
                EmExit( "Невозможно создать выходной файл " + NAME2 );
                goto a306;
            }

            Debug.Print( "Входной файл:" + NAME_IMP_DAT + "" );
            Debug.Print( "Номер сетки=" + KPIECE.ToString() + "" );

            if(KPIECE == 1)
                Debug.Print( String.Format( "IYR,IMON,IDAY={0} {1} {2}",IYR,IMON,IDAY ) );

            Debug.Print( String.Format( "PHI0, AL0, AZ0={0} {1} {2}",PHI0,AL0,AZ0 ) );

            PHI0 = PHI0 / RAD;
            AL0 = AL0 / RAD;
            AZ0 = AZ0 / RAD;
            for(ii = 1; ii <= NETPNT; ii++)
            {
                X1 = XNET[ ii ] / RAD;
                Y1 = YNET[ ii ] / RAD;
                GEDECCON( PHI0,AL0,AZ0,X1,Y1,ref X,ref Y );
                if(emexit == 1)
                    goto a306;//критическая остановка
                XNET[ ii ] = X;
                YNET[ ii ] = Y;
            }
            Debug.Print( "NUMBER OF NET POINTS=" + NETPNT );
            ////////////////////////////////////////////////////////////////////////////
            ///pg3->m_comb_mod_s_eff.GetLBText(pg3->m_comb_mod_s_eff.GetCurSel(),PARFLN);
            ///str="INPUT FILE FOR I-CALCULATIONS - "+PARFLN;str+="";
            ///Debug.Print(str);
            ////////////////////////////////////////////////////////////////////////////
            ///str="Имя файла для перевода M в MW - "+MTOMW;str+="";
            ///Debug.Print(str);
            //PARFLN = "45imr";

            raDT = FillTable( "select * from " + MTOMW + " ORDER BY imw ASC" );

            ii = 1;
            for(int i = 0; i < raDT.Rows.Count; i++)
            {
                OMLH[ ii ] = Convert.ToDouble( raDT.Rows[ i ][ "omlh" ] );
                OMW[ ii ] = Convert.ToDouble( raDT.Rows[ i ][ "omw" ] );
                ii++;
            }

            IMW = ii - 1;

            Debug.Print( String.Format( "CMW1, CMW2, CLW1, CLW2 {0} {1} {2} {3}",CMW1,CMW2,CLW1,CLW2 ) );
            if(CMW1 >= CMW2)
            {
                Debug.Print( "CMW1 ДОЛЖНО БЫТЬ МЕНЬШЕ ЧЕМ CMW2" );
                EmExit( "CMW1 ДОЛЖНО БЫТЬ МЕНЬШЕ ЧЕМ CMW2" );
                goto a306;
            }
            LLOOP = 1;
            MACRR3();
            DST3[ 2 ] = R35 / 2;
            DST3[ 3 ] = R35;
            DST3[ 4 ] = R36;
            DST3[ 5 ] = R37;
            DST3[ 6 ] = R38;
            DST3[ 7 ] = R39;

            /////////////////////////////////////////////////////////////
            //TPR = 1;
            //NCYCL = m_e_iter;
            //NCYCL = 1;
            TMAX = TPR * NCYCL;
            
            /////////////////////////////////////////////////////////////

            Debug.Print( String.Format( "IY0,T,NCYCL,TMAX= {0} {1} {2} {3}",IY0,TPR,NCYCL,TMAX ) );
            IY = IY0;
            aa = "";
            NZ1 = 0;
            /*
                String.Format("select * from Линеаменты ORDER BY ind ASC");
                rs.Open(AFX_DAO_USE_DEFAULT_TYPE,str);
                if(rs.GetRecordCount())rs.MoveFirst();
                while(!rs.IsEOF()){rs.MoveNext();}
                NZ1=lin_kol=rs.GetRecordCount();
                rs.Close();
                
                String.Format("select * from Домены ORDER BY ind ASC");
                rs.Open(AFX_DAO_USE_DEFAULT_TYPE,str);
                if(rs.GetRecordCount())rs.MoveFirst();
                while(!rs.IsEOF()){rs.MoveNext();}
                dom_kol=rs.GetRecordCount();
                NZ1+=rs.GetRecordCount();
                rs.Close();

                String.Format("select * from Очаги ORDER BY ind ASC");
                rs.Open(AFX_DAO_USE_DEFAULT_TYPE,str);
                if(rs.GetRecordCount())rs.MoveFirst();
                while(!rs.IsEOF()){rs.MoveNext();}
                och_kol=rs.GetRecordCount();
                NZ1+=rs.GetRecordCount();
                rs.Close();
            */
            rsDT = FillTable( "select ind from Линеаменты ORDER BY ind ASC" );
            lin_kol = rsDT.Rows.Count;
            rsDT = FillTable( "select ind from Домены ORDER BY ind ASC" );
            dom_kol = rsDT.Rows.Count;
            rsDT = FillTable( "select ind from Очаги ORDER BY ind ASC" );
            och_kol = rsDT.Rows.Count;

            NZ1 = lin_kol + dom_kol + och_kol;

            NZ = 1;
            NEQS = 0;
            NFAIL = 0;
            NGEN = 0;
            //long[ , ] 
            IGST = new double[ NRP + 1,IMGS + 1 ];/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
           // IGST = new long[NRP + 1, IMGS + 1];//

            long iw,Jf,Jx;//,Jq;
            posi = 0;
            double T;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            //задание массива спектров реакций
            ResponseSpectra[] RS = new ResponseSpectra[NETPNT + 1];


            for (int sk = 1; sk <= NETPNT; sk++)//
            {
                RS[sk] = new ResponseSpectra(typeOfGrunt, TMAX);


                //Инициализация массива с длительностями

                for (int i = 0; i <= 60; i++)
                {
                    DurrationMassive[sk-1,0, i + 1] = 5.5 + (double)i / 10;
                    DurrationMassive[sk-1,i + 1, 0] = i;
                }
            }
                        

            ///pMainWnd->m_progress.SetRange(0,100);
            add321:
            
            rdomDT = FillTable( "select * from Домены ORDER BY ind ASC" );
            rlinDT = FillTable("select * from Линеаменты ORDER BY ind ASC");

            NameOfCurrentCalculation = "Расчет по доменам";

            //расчет прогрессбара
            total = rdomDT.Rows.Count + rlinDT.Rows.Count;
            k_progress = 0;
            percents = (k_progress * 100) / total;
            worker.ReportProgress(percents, NameOfCurrentCalculation);


            // начало доменов
            iw = 1;
            for(int j = 0; j < rdomDT.Rows.Count; j++)//начало доменов
            {
                NameOfCurrentCalculation = "Расчет по доменам (" + Convert.ToString(j + 1) + " из " + Convert.ToString(rdomDT.Rows.Count) + " )";
                //изменение положения прогрессбара
                k_progress++;
                percents = (k_progress * 100) / total;
                worker.ReportProgress(percents, NameOfCurrentCalculation);

                posi = (iw * 100) / dom_kol;
                position_rdom = j;
                ///pMainWnd->m_progress.SetPos(posi);
                /*
        tim_cur=tim_cur.GetCurrentTime();
        tim_cur=tim_cur-tim_st;
        String.Format("%02d:%02d:%02d",tim_cur.GetHour(),tim_cur.GetMinute(),tim_cur.GetSecond());
        pg3->m_st_time.SetText(str);
        pg3->m_st_time2.SetText(str);
        String.Format("%d",IND);
        pg3->m_st_voz.SetText(str);
        pg3->UpdateData(0);*/
                // ТЕКУЩЕЕ ВРЕМЯ РАСЧЕТА
                tim_cur = DateTime.Now;
                Application.DoEvents();

                NEQ = 0;
                NFAI = 0;
                NGE = 0;
                IGRFM = new double[ IMM + 1 ];

                IND = Convert.ToInt32( rdomDT.Rows[ position_rdom ][ "ind" ] );
                DeargLineamDoman[iiIND, 0] = IND;//для деагрегации
                
                KMAG = Convert.ToInt32( rdomDT.Rows[ position_rdom ][ "kmag" ] );
                PARFLN = Convert.ToString(rdomDT.Rows[position_rdom]["parfln"]);
                kszon = Convert.ToDouble(rdomDT.Rows[position_rdom]["kszon"]);// площадь домена в км2
                SDEVA = Convert.ToDouble(rdomDT.Rows[position_rdom]["sdeva"]);
                SDEVM = Convert.ToDouble(rdomDT.Rows[position_rdom]["sdevm"]);
                SDEVI = Convert.ToDouble(rdomDT.Rows[position_rdom]["sdevi"]);///////////////////////////////////////////////////////////////////////////////////////
                try
                {
                    typeOfMovement = Convert.ToInt16(rdomDT.Rows[position_rdom]["tip_podv"]);
                }
                
                catch
                {
                    MessageBox.Show("У домена " + Convert.ToString(IND) + " не задана подвижка");
                }

                if (typeOfMovement == 5)
                {
                    typeOfMovement = rnd.Next(0,5);
                }

            //проверка
                LLOOP = 1;
                MACRR3();

                //ITY - здесь = 2
                
                ITY = 2;
                Debug.Print( String.Format( "ZNUMB,TIP,KMAG={0} {1} {2}",IND,ITY,KMAG ) );
                ISBR = 1;
                if(ITY == 2)
                {
                    IGER = 0;
                    PROCVZ(ref ISM);// расчет!
                    if(emexit == 1)
                        goto a306;//критическая остановка
                    if(IGER == 1)
                        goto ad2;
                }




                FIRSTMAGRound = Math.Truncate(FIRSTMAG * 2) / 2;

                if (FIRSTMAGRound <= 5.5)
                {
                    rbb=rbb55;
                }

                if (FIRSTMAGRound  ==6.0)
                {
                    rbb = rbb60;
                }
               if (FIRSTMAGRound == 6.5)
                {
                    rbb = rbb65;
                }
                if (FIRSTMAGRound == 7.0)
                {
                    rbb = rbb70;
                }
                if (FIRSTMAGRound == 7.5)
                {
                    rbb = rbb75;
                }
                if (FIRSTMAGRound == 8.0)
                {
                    rbb = rbb80;
                }




                if (fastCalc == 1)
                    TMAX2 = ((kszon / (3.141592 * rbb * rbb)) / (GR[1])) * NCYCL;
                else
                    TMAX2 = TMAX;

                T = 0.0;
                L0 = new long[ IMM + 1 ];
                ISBR = 2;
ad82:
                if (T > TMAX2)
                    goto ad83;

                NGE++;
                if(ITY == 2)
                {
                    IGER = 0;
                    PROCVZ( ref ISM );// расчет!
                    if(emexit == 1)
                        goto a306;//критическая остановка

                    if(IGER == 1 || IFLT == 1)
                        goto ad101;
                }
                IGRFM[ ISM ] = IGRFM[ ISM ] + 1;
                AMW = SPAR[ 1 ];
                NORMRND( ref UX,ref UY );

                if(emexit == 1)
                    goto a306;//критическая остановка

                DX1 = UX * DEVL * SROT - UY * DEVC * CROT;
                DX2 = UX * DEVL * CROT + UY * DEVC * SROT;
                SPAR[ 9 ] = SPAR[ 9 ] + DX1;
                SPAR[ 10 ] = SPAR[ 10 ] + DX2;
                if(KPCAT == 2)
                {
                    if((((LCAT == 1 && INDC1 <= IND) && IND <= INDC2) && CMAG1 <= AMW) && AMW <= CMAG2)
                    {
                        SPAR[ 6 ] = SPAR[ 6 ] + DX1;
                        SPAR[ 7 ] = SPAR[ 7 ] + DX2;
                        DEGEDCON( PHI0,AL0,AZ0,SPAR[ 6 ],SPAR[ 7 ],ref SPR6,ref SPR7 );
                        if(emexit == 1)
                            goto a306;//критическая остановка

                        SPR4 = (SPAR[ 4 ] + AZ0) * RAD;
                        if(SPR4 >= 360.0)
                            SPR4 = SPR4 - 360.0;
                        SPR5 = SPAR[ 5 ] * RAD;
                        SPR6 = SPR6 * RAD;
                        SPR7 = SPR7 * RAD;
                        ML = MwToMl(AMW);
                        ctl.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t", IND, AMW, SPAR[2], SPAR[3], SPR4, SPR5, SPR6, SPR7, SPAR[8]));//t{1:.0}

                    }
                }
                CLCRB3( IM3,AMW,BSM3,DST3,ref RBALL3 );
                if(emexit == 1)
                    goto a306;//критическая остановка
                NORMRND( ref UI,ref VI );
                if(emexit == 1)
                    goto a306;//критическая остановка

                GLBVI = SDEVM * VI;
                int sk;
                //цикл расчета по точекам сетки
                for (sk = 1; sk <= NETPNT; sk++)//
                {
                    double zz = 0.0;
                    DSTPRG(XNET[sk], YNET[sk], zz, US1, US2, US3, US4, ref RMI);
                    if (emexit == 1)
                        goto a306;//критическая остановка

                    if (RMI < R3DMIN)//   !#############Rmin for source grid
                    {
                        RMI = R3DMIN;
                        CRN = 1.0;
                    } else { CRN = 2.0; };
                    // ПЕРЕСМОТРЕТЬ ТУТ ghbdtltybt nbgjd: (int)                    
                    RPAR[ 5 ] = 2.0 * (Convert.ToInt32( (CRN * SPAR[ 2 ] / 2.0) ) / 2.0) + 1.0;
                    RPAR[ 6 ] = 2.0 * (Convert.ToInt32( (SPAR[ 3 ] / 2.0) / 2.0 )) + 1.0;
                    if(KSTIC == 1)
                        RPAR[ 6 ] = 1.0;
                    XX = XNET[ sk ] - SPAR[ 9 ];
                    YY = YNET[ sk ] - SPAR[ 10 ];
                    RR = Math.Pow( XX,2.0 ) + Math.Pow( YY,2.0 );
                    //RR=XX*XX+YY*YY;
                    R3D = Math.Sqrt( RR + SPAR[ 11 ] * SPAR[ 11 ] );
                    if(R3D < R3DMIN) { R3D = R3DMIN; }
                    if (R3D >= RBALL3)
                        continue;
                    RR = Math.Sqrt( RR );

                    if(RR < 1.0E-5) { RR += .01; }

                   // LLOOP = 1;
                    MACRR3();

              
                    //считается спектр реакций
                    ML = MwToMl(AMW);
                    RS[sk].Calculat(typeOfMovement, ML, DISTMIN);

                    
                    
                    if (emexit == 1)
                        goto a306;//критическая остановка
                    NORMRND( ref UI,ref VI );
                    if(emexit == 1)
                        goto a306;//критическая остановка

/*                    if (BALL <= 4)
                    {
                        SDEVI = 0.015 * BALL * BALL - 0.225 * BALL + 1.41;
                    }
                    else
                    {
                        if (BALL > 7)
                        {
                            SDEVI = -0.005 * BALL * BALL + 0.005 * BALL + 0.71;
                        }
                        else
                        {
                            if (BALL > 6)
                            {
                                SDEVI = -0.22 * BALL + 2.04;
                            }
                            else
                            {
                                SDEVI = 0.015 * BALL * BALL - 0.165 * BALL + 1.17;
                            }
                        }
                    }

*/
                    //Конечный расчет балла
                    if (tipRazbrosa == 0)
                    {
                        RESI = BALL + UI * SDEVI + GLBVI;
                    }
                    else
                    {
                        var round_BALL = Convert.ToInt16(Math.Round(BALL, MidpointRounding.AwayFromZero));

                        if (round_BALL <= 2)
                            RESI = BALL + normRand.NextDouble() * I_dependency[0] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 3)
                            RESI = BALL + normRand.NextDouble() * I_dependency[1] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 4)
                            RESI = BALL + normRand.NextDouble() * I_dependency[2] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 5)
                            RESI = BALL + normRand.NextDouble() * I_dependency[3] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 6)
                            RESI = BALL + normRand.NextDouble() * I_dependency[4] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 7)
                            RESI = BALL + normRand.NextDouble() * I_dependency[5] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 8)
                            RESI = BALL + normRand.NextDouble() * I_dependency[6] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL >= 9)
                            RESI = BALL + normRand.NextDouble() * I_dependency[7] + normRand.NextDouble() * SDEVM;
                    }
                    
                    //считаем длительность

                        if (RESI >= 5.5 && RS[sk].Duration < 60)
                        {
                            FillingDuration(RESI, RS[sk].Duration, sk - 1);
                        }


                                       
                    
                    
                    
                    // RESI = BALL + UI * SDEVI + GLBVI;////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    
                    if (RESI < GI0) { IGIST = 1; }
                    else
                    {
                        IGIST = (int)((RESI - GI0) / DI) + 2;
                    };


                    if (DEAG == 1)
                    {

                        if (AMW == 3.82) { ideg = 1; }
                        if (AMW == 4.25) { ideg = 2; }
                        if (AMW == 4.62) { ideg = 3; }
                        if (AMW == 5.02) { ideg = 4; }
                        if (AMW == 5.43) { ideg = 5; }
                        if (AMW == 5.83) { ideg = 6; }
                        if (AMW == 6.23) { ideg = 7; }
                        if (AMW == 6.63) { ideg = 8; }
                        if (AMW == 7.00) { ideg = 9; }
                        if (AMW == 7.50) { ideg = 10; }
                        if (AMW == 8.00) { ideg = 11; }


                        if (RESI < balldeagr1[sk]) { }
                        else  ////////////////////деагрегация
                        {

                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                               // DEAGREG[3, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 1]++;
                            }
                                                        
                        }

                        if (RESI < balldeagr2[sk]) { }
                        else  ////////////////////деагрегация
                        {
                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                              //  DEAGREG[4, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 2]++;
                            }
                                                        
                        }

                        if (RESI < balldeagr3[sk]) { }
                        else  ////////////////////деагрегация
                        {
                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                               // DEAGREG[5, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 3]++;
                            }
                        }

                        if (RESI < balldeagr4[sk]) { }
                        else  ////////////////////деагрегация
                        {
                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                             //   DEAGREG[6, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 4]++;
                            }
                        }

                        if (RESI < balldeagr5[sk]) { }
                        else  ////////////////////деагрегация
                        {
                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                            //    DEAGREG[7, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 5]++;
                            }
                            
                        }


                        if (RESI < balldeagr6[sk]) { }
                        else  ////////////////////деагрегация
                        {
                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                             //   DEAGREG[8, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 6]++;
                            }
                            
                        }

                        if (RESI < balldeagr7[sk]) { }
                        else  ////////////////////деагрегация
                        {
                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                              //  DEAGREG[9, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 7]++;
                            }
                            
                        }
                        if (DISTMIN <= 350)
                            currentDeag.RESI_deag(ML, DISTMIN, RESI, sk-1);

                    //Деагрегация по спектрам реакций
                        if (RS[sk].fl)
                        {
                            //считаем для PGA
                            double freq = 0;
                            //входные 
                          //  DeagregForRS(RS[sk].PGA, PGA_deagreg, sk, ideg, 3);

                            //Деагрегация для всех периодов
                            if (DISTMIN<=350)
                                currentDeag.SA_deag(ML, DISTMIN, RS[sk].Bitog, RS[sk].PGA, sk - 1);
                            RS[sk].fl = false;
                        }


                    }

                    
                    if(IGIST > IMGS) { IGIST = IMGS; }
                    k22 = IGIST;
                    if (fastCalc == 1)
                        IGST[sk, k22] = IGST[sk, k22] + (KOEFF[ISM] * (TMAX/TMAX2));
                    else
                        IGST[sk, k22]++;

                };
                goto ad90;
ad101:
                NFAI++;
                EXPRND( ref IY,SLD,ref DT );
                if(emexit == 1)
                    goto a306;//критическая остановка
                T += (float)DT;
                goto ad82;
ad90:
                NEQ++;
                EXPRND( ref IY,SLD,ref DT );
                if (emexit == 1)
                    
                    goto a306;//критическая остановка
                T += (float)DT;
                goto ad82;
ad83:


                IGRFM[ INMAX + 1 ] = 0.0;
                for(Jf = INMAX; Jf >= 1; Jf--)
                    IGRFM[ Jf ] += IGRFM[ Jf + 1 ];

                for(Jx = 1; Jx <= INMAX; Jx++)
                {
                    GRFM[ Jx ] = IGRFM[ Jx ] / (SZON * TMAX2);
                    grp.WriteLine( String.Format( "{0} {1} {2}",MGNT[ Jx ],GR[ Jx ],GRFM[ Jx ] ) );

                    if(m_main_base_ok)
                    {
                        //str = String.Format( "INSERT INTO Зоны_ВОЗ (ind , nomer , mlh , realn , model) VALUES ({0},{1},{2},{3},{4})", IND,Jx,MGNT[ Jx ],GR[ Jx ],GRFM[ Jx ] );
                        InsertCommand( str );
                    }
                }
                NEQS = Convert.ToInt64( NEQ ) + NEQS;
                NFAIL = NFAI + NFAIL;
                NGEN = Convert.ToInt64( NGE ) + NGEN;
                Debug.Print( String.Format( "NGEN,NEQS,NFAIL = {0}\t{1}\t{2}",NGE,NEQ,NFAI ) );
                NZ++;
                iw++;

                iiIND++;
                ///rdom.MoveNext();
                continue;

ad2:
                if(IGER == 1)
                {
                    NZ++;
                    iw++;
                    iiIND++;
                    ///rdom.MoveNext();
                    continue;
                }
                
            };
            rdomDT.Clear();

            ///	pMainWnd->m_progress.SetPos(0);

            //начало линеаментов
            

            iw = 1;
            for(li = 0; li < rlinDT.Rows.Count; li++)
            {
                //изменение положения прогрессбара
                NameOfCurrentCalculation = "Расчет по линеаментам (" + Convert.ToString(li + 1) + " из " + Convert.ToString(rlinDT.Rows.Count) + " )";
                k_progress++;
                percents = (k_progress * 100) / total;
                worker.ReportProgress(percents, NameOfCurrentCalculation);

                posi = (iw * 100) / lin_kol;
                ///		pMainWnd->m_progress.SetPos(posi);

                tim_cur = DateTime.Now;
                /*
                    tim_cur=tim_cur-tim_st;
                    String.Format("%02d:%02d:%02d",tim_cur.GetHour(),tim_cur.GetMinute(),tim_cur.GetSecond());
                    pg3->m_st_time.SetText(str);
                    pg3->m_st_time2.SetText(str);
                    String.Format("%d",IND);
                    pg3->m_st_voz.SetText(str);
                    pg3->UpdateData(0);*/
                Application.DoEvents();

                NEQ = 0;
                NFAI = 0;
                NGE = 0;

                IGRFM = new double[ IMM + 1 ];
                
                IND = Convert.ToInt64( rlinDT.Rows[ li ][ "ind" ] );
                DeargLineamDoman[iiIND, 0] = IND;//для деагрегации

                KMAG = Convert.ToInt64( rlinDT.Rows[ li ][ "kmag" ] );
                PARFLN = Convert.ToString(rlinDT.Rows[li]["parfln"]);
                SDEVA = Convert.ToDouble(rlinDT.Rows[li]["sdeva"]);
                SDEVM = Convert.ToDouble(rlinDT.Rows[li]["sdevm"]);
                SDEVI = Convert.ToDouble(rlinDT.Rows[li]["sdevi"]);
                try
                {
                    typeOfMovement = Convert.ToInt16(rlinDT.Rows[li]["tip_podv"]);
                }                
                 catch
                {
                    MessageBox.Show("У линеамента " + Convert.ToString(IND) + " не задана подвижка");
                }

                if (typeOfMovement == 5)
                {
                    typeOfMovement = rnd.Next(0, 5);
                }

                //ITY - здесь = 1
                ITY = 1;
                LLOOP = 1;
                MACRR3();
                Debug.Print( String.Format( "ZNUMB,TIP,KMAG={0} {1} {1}",IND,ITY,KMAG ) );
                ISBR = 1;
                if(ITY == 1)
                {
                    IGER = 0;
                    PROCFZ( ref ISM );
                    if(emexit == 1)
                        goto a306;//критическая остановка

                    if(IGER == 1) { goto al2; }
                } else if(ITY == 2)
                {
                    IGER = 0;
                    PROCVZ( ref ISM );
                    if(emexit == 1)
                        goto a306;//критическая остановка

                    if(IGER == 1) { goto al2; }
                } else
                {
                    IGER = 0;
                    PROCLR( ref ISM );
                    if(emexit == 1)
                        goto a306;//критическая остановка

                    if(IGER == 1) { goto al2; }
                }
                T = 0.0;
                L0 = new long[ IMM + 1 ];
                ISBR = 2;
al82:
                if(T > TMAX) { goto al83; }
                NGE++;
                if(ITY == 2)
                {
                    IGER = 0;
                    PROCVZ( ref ISM );
                    if(emexit == 1)
                        goto a306;//критическая остановка

                    if(IGER == 1) { goto al101; }
                    if(IFLT == 1) { goto al101; }
                } else if(ITY == 1)
                {
                    IGER = 0;
                    PROCFZ( ref ISM );
                    if(emexit == 1)
                        goto a306;//критическая остановка

                    if(IGER == 1) { goto al101; }
                    if(IFLT == 1) { goto al101; }
                } else
                {
                    IGER = 0;
                    PROCLR( ref ISM );
                    if(emexit == 1)
                        goto a306;//критическая остановка

                    if(IGER == 1) { goto al101; }
                    if(IFLT == 1) { goto al101; }
                }
                IGRFM[ ISM ] = IGRFM[ ISM ] + 1;
                AMW = SPAR[ 1 ];
                NORMRND( ref UX,ref UY );
                if(emexit == 1)
                    goto a306;//критическая остановка

                DX1 = UX * DEVL * SROT - UY * DEVC * CROT;
                DX2 = UX * DEVL * CROT + UY * DEVC * SROT;
                SPAR[ 9 ] = SPAR[ 9 ] + DX1;
                SPAR[ 10 ] = SPAR[ 10 ] + DX2;
                if(KPCAT == 2)
                {
                    if((((LCAT == 1 && INDC1 <= IND) && IND <= INDC2) && CMAG1 <= AMW) && AMW <= CMAG2)
                    {
                        SPAR[ 6 ] = SPAR[ 6 ] + DX1;
                        SPAR[ 7 ] = SPAR[ 7 ] + DX2;
                        DEGEDCON( PHI0,AL0,AZ0,SPAR[ 6 ],SPAR[ 7 ],ref SPR6,ref SPR7 );
                        if(emexit == 1)
                            goto a306;//критическая остановка

                        SPR4 = (SPAR[ 4 ] + AZ0) * RAD;
                        if(SPR4 >= 360.0)
                            SPR4 = SPR4 - 360.0;
                        SPR5 = SPAR[ 5 ] * RAD;
                        SPR6 = SPR6 * RAD;
                        SPR7 = SPR7 * RAD;
                        ctl.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t", IND, AMW, SPAR[2], SPAR[3], SPR4, SPR5, SPR6, SPR7, SPAR[8]));
                    }
                }

                CLCRB3( IM3,AMW,BSM3,DST3,ref RBALL3 );
                if(emexit == 1)
                    goto a306;//критическая остановка
                NORMRND( ref UI,ref VI );
                if(emexit == 1)
                    goto a306;//критическая остановка

                GLBVI = SDEVM * VI;
                int sk;
                // цикл расчета по точекам сетки
                for (sk = 1; sk <= NETPNT; sk++)
                {
                    double zz = 0.0;
                    DSTPRG( XNET[ sk ],YNET[ sk ],zz,US1,US2,US3,US4,ref RMI );
                    if(emexit == 1)
                        goto a306;//критическая остановка

                    if(RMI < R3DMIN)//   !#############Rmin for source grid
                    {
                        RMI = R3DMIN;
                        CRN = 1.0;
                    } else { CRN = 2.0; };

                    
                    RPAR[ 5 ] = 2.0 * (Convert.ToInt32( CRN * SPAR[ 2 ] / 2.0 ) / 2.0) + 1.0;
                    RPAR[ 6 ] = 2.0 * (Convert.ToInt32( SPAR[ 3 ] / 2.0 ) / 2.0) + 1.0;
                    if(KSTIC == 1)
                        RPAR[ 6 ] = 1.0;
                    XX = XNET[ sk ] - SPAR[ 9 ];
                    YY = YNET[ sk ] - SPAR[ 10 ];
                    RR = Math.Pow( XX,2.0 ) + Math.Pow( YY,2.0 );
                    R3D = Math.Sqrt( RR + SPAR[ 11 ] * SPAR[ 11 ] );
                    if(R3D < R3DMIN) { R3D = R3DMIN; }


                    if (R3D >= RBALL3)
                        continue;

                    RR = Math.Sqrt( RR );
                    if(RR < 1.0e-5) { RR += .01; }

                    MACRR3();


                    //считается спектр реакций
                    ML = MwToMl(AMW);
                    RS[sk].Calculat(typeOfMovement, ML, DISTMIN);

                    if (emexit == 1)
                        goto a306;//критическая остановка
                    NORMRND( ref UI,ref VI );
                    if(emexit == 1)
                        goto a306;//критическая остановка
                                  /*
                                                      if(BALL <= 4)
                                                      {
                                                          SDEVI = 0.015 * BALL * BALL - 0.225 * BALL + 1.41;
                                                      }
                                                      else
                                                      {
                                                          if (BALL > 7)
                                                          {
                                                              SDEVI = -0.005 * BALL * BALL + 0.005 * BALL + 0.71;
                                                          }
                                                          else
                                                          {
                                                              if (BALL > 6)
                                                              {
                                                                  SDEVI = -0.22 * BALL + 2.04;
                                                              }
                                                              else
                                                              {
                                                                  SDEVI = 0.015 * BALL * BALL - 0.165 * BALL + 1.17;                             
                                                              }
                                                          }                    
                                                      }

                                  */
//Конечный расчет балла
                    if (tipRazbrosa == 0)
                    {
                        RESI = BALL + UI * SDEVI + GLBVI;
                    }
                    else
                    {
                        var round_BALL = Convert.ToInt16(Math.Round(BALL, MidpointRounding.AwayFromZero));

                        if (round_BALL <= 2)
                            RESI = BALL + normRand.NextDouble() * I_dependency[0] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 3)
                            RESI = BALL + normRand.NextDouble() * I_dependency[1] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 4)
                            RESI = BALL + normRand.NextDouble() * I_dependency[2] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 5)
                            RESI = BALL + normRand.NextDouble() * I_dependency[3] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 6)
                            RESI = BALL + normRand.NextDouble() * I_dependency[4] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 7)
                            RESI = BALL + normRand.NextDouble() * I_dependency[5] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL == 8)
                            RESI = BALL + normRand.NextDouble() * I_dependency[6] + normRand.NextDouble() * SDEVM;
                        else if (round_BALL >= 9)
                            RESI = BALL + normRand.NextDouble() * I_dependency[7] + normRand.NextDouble() * SDEVM;
                    }

                    //считаем длительность

                    if ( RESI >= 5.5 && RS[sk].Duration < 60)
                    {
                        FillingDuration(RESI, RS[sk].Duration, sk - 1);
                    }

                    if (RESI < GI0)
                        IGIST = 1;
                    else
                        IGIST = (int)((RESI - GI0) / DI) + 2;

                    if (DEAG == 1)
                    {
                        if (AMW == 3.82) { ideg = 1; }
                        if (AMW == 4.25) { ideg = 2; }
                        if (AMW == 4.62) { ideg = 3; }
                        if (AMW == 5.02) { ideg = 4; }
                        if (AMW == 5.43) { ideg = 5; }
                        if (AMW == 5.83) { ideg = 6; }
                        if (AMW == 6.23) { ideg = 7; }
                        if (AMW == 6.63) { ideg = 8; }
                        if (AMW == 7.00) { ideg = 9; }
                        if (AMW == 7.50) { ideg = 10; }
                        if (AMW == 8.00) { ideg = 11; }

                        if (RESI < balldeagr1[sk]) ////////////////////деагрег
                        {
                        }
                        else
                        {

                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71) ////////////////////деагрег
                            {
                              //  DEAGREG[3, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                DeargLineamDoman[iiIND, 1]++;
                            }
                            
                        };

                        if (RESI < balldeagr2[sk]) { }
                        else  ////////////////////деагрегация
                        {

                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                              //  DEAGREG[4, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 2]++;
                            }
                            
                        }

                        if (RESI < balldeagr3[sk]) { }
                        else  ////////////////////деагрегация
                        {
                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                              //  DEAGREG[5, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 3]++;
                            }
                            
                        }

                        if (RESI < balldeagr4[sk]) { }
                        else  ////////////////////деагрегация
                        {
                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                              //  DEAGREG[6, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 4]++;
                            }
                            
                        }

                        if (RESI < balldeagr5[sk]) { }
                        else  ////////////////////деагрегация
                        {
                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                              //  DEAGREG[7, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 5]++;
                            }
                            
                        }


                        if (RESI < balldeagr6[sk]) { }
                        else  ////////////////////деагрегация
                        {
                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                               // DEAGREG[8, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 6]++;
                            }
                            
                        }

                        if (RESI < balldeagr7[sk]) { }
                        else  ////////////////////деагрегация
                        {
                            jdeg = (int)(R3D / 5);
                            if (jdeg < 71)
                            {
                              //  DEAGREG[9, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                                //ideg=(ideg - 1) * 70 + jdeg + 1;
                                DeargLineamDoman[iiIND, 7]++;
                            }
                            
                        }
                        if (DISTMIN <= 350)
                            currentDeag.RESI_deag(ML, DISTMIN, RESI, sk - 1);

                        //Деагрегация по спектрам реакций
                        if (RS[sk].fl)
                        {
                            //считаем для PGA
                            double freq = 0;
                            //входные 
                          //  DeagregForRS(RS[sk].PGA, PGA_deagreg, sk, ideg, 3);

                            //Деагрегация для всех периодов
                            if (DISTMIN <= 350)
                                currentDeag.SA_deag(ML, DISTMIN, RS[sk].Bitog, RS[sk].PGA, sk - 1);
                            RS[sk].fl = false;
                        }



                    }
                    if(IGIST > IMGS)
                        IGIST = IMGS;
                    k22 = IGIST;
                    IGST[ sk,k22 ]++;
                    //IGST[ sk,k22 ] = IGST[ sk,k22 ] + 1;

                };

                goto al90;
al101:
                NFAI++;
                EXPRND( ref IY,SLD,ref DT );
                if(emexit == 1)
                    goto a306;//критическая остановка

                T += (float)DT;
                goto al82;
al90:
                NEQ++;
                EXPRND( ref IY,SLD,ref DT );
                if(emexit == 1)
                    goto a306;//критическая остановка

                T += (float)DT;
                goto al82;
al83:


                IGRFM[ INMAX + 1 ] = 0.0;
                for(Jf = INMAX; Jf >= 1; Jf--)
                    IGRFM[ Jf ] += IGRFM[ Jf + 1 ];

                for(Jx = 1; Jx <= INMAX; Jx++)
                {
                    GRFM[ Jx ] = IGRFM[ Jx ] / (SZON * TMAX);
                    grp.WriteLine( String.Format( "{0}\t{1}\t{2}\t",MGNT[ Jx ],GR[ Jx ],GRFM[ Jx ] ) );

                    if(m_main_base_ok)
                    {
                        InsertCommand( String.Format( "INSERT INTO Зоны_ВОЗ (ind , nomer , mlh , realn , model) VALUES ({0},{1},{2},{3},{4})",IND,Jx,MGNT[ Jx ],GR[ Jx ],GRFM[ Jx ] ) );
                        ///maindb.Execute( str );
                    }
                }
                NEQS = NEQ + NEQS;
                NFAIL = NFAI + NFAIL;
                NGEN = NGE + NGEN;
                Debug.Print( String.Format( "NGEN,NEQS,NFAIL = {0}\t{1}\t{2}",NGE,NEQ,NFAI ) );
                NZ++;
                iw++;
                iiIND++;
                ///rlin.MoveNext();
                continue;
al2:
                if(IGER == 1)
                {
                    NZ++; iw++;
                    iiIND++;
                    ///rlin.MoveNext();
                    continue;
                }
            };

            rlinDT.Clear();
 


            ///	pMainWnd->m_progress.SetPos(0);
            //конец линеаментов

            ///pMainWnd->m_progress.SetPos(0);
            ////конец очагов

            NZ = NZ - 1;
            Debug.Print( "число зон = " + NZ );

            if(NZ == 0)
            {
                Debug.Print( "FOR KPIECE = " + KPIECE );
                Debug.Print( "ALL ZONES EXCLUDED" );
                KPIECE = KPIECE + 1;
                goto a307;
            }

            grp.Close();
            Debug.Print( String.Format( "NGENT, NEQST, NFAILT = {0} {1} {2}",NGEN,NEQS,NFAIL ) );
            
            if(KPCAT == 2)
                if(LCAT == 1)
                    ctl.Close();
            
            NAME4 = PrefixName + PRBL[ KPIECE - 1 ] + "_GST.TXT";
            try
            {
                //gst.Open(NAME4,CFile::modeWrite|CFile::typeText|CFile::modeCreate);
                gst = new StreamWriter( NAME4,false,Encoding.GetEncoding( 1251 ) );
            }
            catch(Exception ex)
            {
                MessageBox.Show( "ERROR: " + ex.Message );
                return;
            }

            if(KMOD == 1)
            {
                //NAME3 = PrefixName + PRBL[ KPIECE - 1 ] + "_B.TXT";
                //NAME5 = PrefixName + PRBL[ KPIECE - 1 ] + "_C.TXT";
                //NAME8 = PrefixName + PRBL[ KPIECE - 1 ] + "_D.TXT";
                NAMEA = PrefixName + PRBL[ KPIECE - 1 ] + "_ABCD.TXT";
                try
                {
                    //id5 = new StreamWriter( NAME3,false,Encoding.GetEncoding( 1251 ) );
                    //res = new StreamWriter( NAME5,false,Encoding.GetEncoding( 1251 ) );
                    //im2 = new StreamWriter( NAME8,false,Encoding.GetEncoding( 1251 ) );
                    fla = new StreamWriter( NAMEA,false,Encoding.GetEncoding( 1251 ) );
                }
                catch(Exception ex)
                {
                    MessageBox.Show( "ERROR: " + ex.Message );
                    return;
                }
            }
            GSTPRC(worker, NETPNT,PHI0,AL0,AZ0,NCYCL,XNET,YNET,GI0,TMAX , RS);
            if(emexit == 1)
                goto a306;//критическая остановка
            Debug.Print( "IY = " + IY.ToString() );
            ct_endwork = DateTime.Now;
            TimeSpan TS = ct_endwork - ct;

            day = Convert.ToInt32( TS.TotalDays );
            hours = Convert.ToInt32( TS.TotalHours );
            min = Convert.ToInt32( TS.TotalMinutes );
            sec = Convert.ToInt32( TS.TotalSeconds );

            Debug.Print( TS.ToString() );
            ///pg3->m_st_time2.SetText( str );
            if(KMOD == 1)
                fla.Close();

            gst.Close();

            goto a307;

            net.Close();
a306:

            if (fla != null)
                if(fla.BaseStream != null)
                    fla.Close();

            
            if(outFile != null)
                if(outFile.BaseStream != null)
                    outFile.Close();

            if(wrng_out != null)
                if(wrng_out.BaseStream != null)
                    wrng_out.Close();



            //POVTOR_BALL.Close();
        }

        private void FillingDuration(double rESI, double duration,int pointNumber)
        {
            try
            {
                
               
                round_i = Convert.ToInt16( Math.Round(duration, MidpointRounding.AwayFromZero))+1;
                round_j = Convert.ToInt16(Math.Round((rESI - 5.5) * 10 + 1, MidpointRounding.AwayFromZero));
                if(round_i < 62 && round_j < 62)
                {
                    DurrationMassive[pointNumber, round_i, round_j]++;
                }
                

            }
            catch
            {
                MessageBox.Show("Ошибка в функции заполнения длительности");
            }
            
        }

        private void SaveDurationMassive()
        {
            String NameDIR;
            NameDIR = Application.StartupPath;

            //Если директория существует - удалить её
            if (Directory.Exists(NameDIR + "\\duration"))
            {
                try
                {
                    Directory.Delete(NameDIR + "\\duration");
                    System.Threading.Thread.Sleep(1000);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Невозможно удалить папку с длительностями\nОписание:\n"+ex.Message, "Ошибка");
                }
            }

            try
            {
                Directory.CreateDirectory(NameDIR + "\\duration");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Невозможно создать папку с длительностями\nОписание:\n" + ex.Message, "Ошибка");
            }
            
            for (int num = 0;num< NETPNT; num++)
            {
                try
                {
                    StreamWriter Filewriter = new StreamWriter(NameDIR + "\\duration\\" + (num + 1) + "_point.txt");
                    for (int i = 0; i <= 61; i++)
                    {
                        for (int j = 0; j <= 61; j++)
                        {
                            Filewriter.Write("{0}\t", DurrationMassive[num, i, j]);
                        }
                        Filewriter.Write("\n");
                    }

                    Filewriter.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Невозможно сохранить файлы с длительностями\nОписание:\n" + ex.Message, "Ошибка");
                }

            }

 
        }

        private void FillOtherTables()
        {
            InsertCommand( "delete * from VII" );
            InsertCommand( "delete * from VIII" );
            InsertCommand( "delete * from IX" );

            DataTable rsDT = new DataTable( "rsDT" );
            double lat,lon,igs;
            int ind;
            double ball = 0.0,b_prom = 0.0;
            i_gst2 = 0;

            for (int i = 0; i < mass_gist_lat_lon.Length; i++)
            {
                ind = 0;
                lat = 0.0;
                lon = 0.0;
                ball = 0.0;
                igs = 0.0;

                if(i_gst2 == i) break;

                ind = i + 1;
                lat = mass_gist_lat_lon[ i,0 ];
                lon = mass_gist_lat_lon[ i,1 ];
               // POVTOR[0,i]=lat;
                // POVTOR[1,i]=lon;

               try
               {
               rsDT = FillTable( "select * from Гистограммы where ind=" + ind + " ORDER BY ball ASC" );

                for (int j = 0; j < rsDT.Rows.Count; j++)
                {
                    b_prom = Convert.ToDouble(rsDT.Rows[j]["ball"]);

                    igs = 0.0;
                    ball = 0.0;

                    if (b_prom == 6.0)
                    {
                        igs = Convert.ToDouble(rsDT.Rows[j]["igs"]);
                        if (igs != 0)
                        {
                            ball = m_e_tmax / (m_e_iter * igs);
                      //      POVTOR[2,i] = ball;
                        }
                    }

                    if (b_prom == 7.0)
                    {
                        igs = Convert.ToDouble(rsDT.Rows[j]["igs"]);
                        if (igs != 0)
                        {
                            ball = m_e_tmax / (m_e_iter * igs);
                       //     POVTOR[3,i] = ball;
                        }
                    }

                    if (b_prom == 8.0)
                    {
                        igs = Convert.ToDouble(rsDT.Rows[j]["igs"]);
                        if (igs != 0)
                        {
                            ball = m_e_tmax / (m_e_iter * igs);
                       //     POVTOR[4,i] = ball;
                        }
                    }

                    if (b_prom == 9.0)
                    {
                        igs = Convert.ToDouble(rsDT.Rows[j]["igs"]);
                        if (igs != 0)
                        {
                            ball = m_e_tmax / (m_e_iter * igs);
                       //     POVTOR[5,i] = ball;
                        }
                    }
                  //  POVTOR_BALL.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", POVTOR[i, 0], POVTOR[i, 1], POVTOR[i, 2], POVTOR[i, 3], POVTOR[i, 4], POVTOR[i, 5]));
                }




                rsDT.Clear();
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
            }

       
        }

        private void EmExit(string p)
        {
            MessageBox.Show( p );
        }

        private void GEDECCON(double f0,double al0,double az0,double f,double al,ref double x,ref double y)
        {
            //C  geodezicheskie v dekartovy (konicheskie)
            //ff1(x)=6367.5584958746*x-16.0364802690885*Math.Sin(2*x)+0.0168280667831*Math.Sin(4*x)-0.0000219752790*Math.Sin(6*x)+0.0000000311243*Math.Sin(8*x);
            double c = 10374.71;
            double alf = 0.854116;
            double qu = c - (6367.5584958746 * f0 - 16.0364802690885 * Math.Sin( 2 * f0 ) + 0.0168280667831 * Math.Sin( 4 * f0 ) - 0.0000219752790 * Math.Sin( 6 * f0 ) + 0.0000000311243 * Math.Sin( 8 * f0 ));
            double ro = c - (6367.5584958746 * f - 16.0364802690885 * Math.Sin( 2 * f ) + 0.0168280667831 * Math.Sin( 4 * f ) - 0.0000219752790 * Math.Sin( 6 * f ) + 0.0000000311243 * Math.Sin( 8 * f ));
            double delt = alf * (al - al0);
            double x1 = ro * Math.Sin( delt );
            double y1 = qu - ro * Math.Cos( delt );
            double sn = Math.Sin( az0 );
            double cs = Math.Cos( az0 );
            x = x1 * cs - y1 * sn;
            y = x1 * sn + y1 * cs;
        }

        private void DEGEDCON(double f0,double al0,double az0,double x,double y,ref double f,ref double al)
        {
            //C  dekartovy (konicheskie) v geodezicheskie
            //	s(a)=6367.5584958746*a-16.0364802690885*Math.Sin(2*a)+0.0168280667831*Math.Sin(4*a)-0.0000219752790*Math.Sin(6*a)+0.0000000311243*Math.Sin(8*a);
            double rcros = 6367.5584958746;
            double c = 10374.71;
            double alf = 0.854116;
            double qu = c - (6367.5584958746 * f0 - 16.0364802690885 * Math.Sin( 2 * f0 ) +
                0.0168280667831 * Math.Sin( 4 * f0 ) - 0.0000219752790 * Math.Sin( 6 * f0 ) +
                0.0000000311243 * Math.Sin( 8 * f0 ));
            double x1 = x * Math.Cos( az0 ) + y * Math.Sin( az0 );
            double y1 = -x * Math.Sin( az0 ) + y * Math.Cos( az0 );
            double ros = ((qu - y1) * (qu - y1)) + (x1 * x1);
            double ro = Math.Sqrt( ros );
            f = (c - ro) / rcros + 0.0024412912;
            double sn = x1 / ro;
            double delt = Math.Asin( sn );
            al = delt / alf + al0;
        }

        private void MACRR3()
        {
            double zz = 0.0,xx = 1.0;

            if (LLOOP == 1)
            {  //  goto a99;

                try
                {
                    DataTable rprfDT = new DataTable("rprfDT");
                  
                    rprfDT = FillTable("select * from Сейсм_эффект");
                    
                    
                    int i = 0;

                    while (i < rprfDT.Rows.Count)
                    
                    {
                        IndEffect = Convert.ToString(rprfDT.Rows[i]["ind"]);
                        if (PARFLN == IndEffect)
                            break;
                        i++;
                    }

                    if (i >= rprfDT.Rows.Count)
                    {
                        MessageBox.Show("Не найден сейсмический эффект " + PARFLN, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        Environment.Exit(0);

                    }
            
                    DIDA = Convert.ToDouble(rprfDT.Rows[i]["dida"]);
                    DIDMW = Convert.ToDouble(rprfDT.Rows[i]["didmw"]);
                    CMAG = Convert.ToDouble(rprfDT.Rows[i]["cmag"]);
                    AN1 = Convert.ToDouble(rprfDT.Rows[i]["an1"]);
                    AN2 = Convert.ToDouble(rprfDT.Rows[i]["an2"]);
                    RSWITCH = Convert.ToDouble(rprfDT.Rows[i]["rswitch"]);
                    RQ1 = Convert.ToDouble(rprfDT.Rows[i]["rq1"]);
                    RQ2 = Convert.ToDouble(rprfDT.Rows[i]["rq2"]);
                    R35 = Convert.ToDouble(rprfDT.Rows[i]["r35"]);
                    R36 = Convert.ToDouble(rprfDT.Rows[i]["r36"]);
                    R37 = Convert.ToDouble(rprfDT.Rows[i]["r37"]);
                    R38 = Convert.ToDouble(rprfDT.Rows[i]["r38"]);
                    R39 = Convert.ToDouble(rprfDT.Rows[i]["r39"]);
                    AMLHBAS = Convert.ToDouble(rprfDT.Rows[i]["amlhbas"]);
                    AMWBAS = Convert.ToDouble(rprfDT.Rows[i]["amwbas"]);
                    RBAS = Convert.ToDouble(rprfDT.Rows[i]["rbas"]);
                    AIBAS = Convert.ToDouble(rprfDT.Rows[i]["aibas"]);
                    ALBYWB = Convert.ToDouble(rprfDT.Rows[i]["albywb"]);
                    GI0 = Convert.ToDouble(rprfDT.Rows[i]["gi0"]);
                    tipRazbrosa = Convert.ToInt32(rprfDT.Rows[i]["Tip_rasbrosa"]);


                }
                catch (Exception ex)
                {
                    return;
                }

                ATTPAR[1] = AN1;
                ATTPAR[2] = RQ1;
                ATTPAR[3] = RSWITCH;
                ATTPAR[4] = AN2;
                ATTPAR[5] = RQ2;
                //RSWITCH = Math.Pow((double)10.0, (0.413 * AMWBAS - 1.204));
                CC = CATT(RSWITCH, AN1, RQ1) / CATT(RSWITCH, AN2, RQ2);
                RA = RSWITCH * 0.85;
                RB = RSWITCH * 1.15;
                DR = RB - RA;
                SB = Math.Pow((double)10.0, (AMWBAS - CMAG));
                if (AMWBAS < CMW1) { ALBYWB = CLW1; }
                if (AMWBAS < CMW2 && AMWBAS >= CMW1) { ALBYWB = CLW1 + (CLW2 - CLW1) * (AMWBAS - CMW1) / (CMW2 - CMW1); }
                if (AMWBAS >= CMW2) { ALBYWB = CLW2; }
                ALB = Math.Sqrt(ALBYWB * SB);//длинна базового очага
                AWB = SB / ALB; // ширина базового очага
                DL = 2;// размер элементарного источника 5км
                DW = DL;
                NLB = ALB / DL; // 
                if (NLB > 199) { NLB = 301; }
                NLB = (NLB / 2) * 2 + 1;//      ! ODD GRID SIZE ALONG L
                NWB = AWB / DW;
                if (NWB > 99) { NWB = 201; }
                NWB = (int)((NWB / 2) * 2 + 1);//       ! SAME ALONG W
                HB = 0;
                PHIB = 0;
                RNLB = (double)NLB + 0.0;
                RNWB = (double)NWB + 0.0;
                RPAR[1] = ALB;
                RPAR[2] = AWB;
                RPAR[3] = HB;
                RPAR[4] = PHIB;
                RPAR[5] = RNLB;
                RPAR[6] = RNWB;
                FINCOR(RBAS, zz, xx, ref DISTMIN, ref FCORB, AMWBAS);
                DENOM = FCORB * ATT(RBAS, AMWBAS);

            }
            //return;

            if (LLOOP == 0)
            {
            CBET = (XX * CROT - YY * SROT) / RR;// !INPUT FOR FINCORR
            SBET = (XX * SROT + YY * CROT) / RR;// !SAME
            FINCOR(RR, SBET, CBET, ref DISTMIN, ref FCOR, AMW);
                //DISTMIN
           BALL = AIBAS + DIDMW * (AMW - AMWBAS) + DIDA * Math.Log10((FCOR * ATT(R3D, AMW)) / DENOM);
            }
                LLOOP = 0;            
            return;
        }

        private double minimum(double a,double b)
        {
            return (a < b) ? a : b;
        }

        private void FINCOR(double r, double SBEt, double CBEt, ref double AMINDIST, ref double corr, double mag)
        {
            double AL,AW,H,PHI,RNL,RNW;
            AL = RPAR[ 1 ];
            AW = RPAR[ 2 ];
            H = RPAR[ 3 ];
            PHI = RPAR[ 4 ];
            RNL = RPAR[ 5 ];
            RNW = RPAR[ 6 ];
            NL = RNL;
            NL2 = NL / 2;
            NW = RNW;
            NW2 = NW / 2;
            DL = AL / NL;
            DW = AW / NW;
            XRC = r * CBEt;
            YRC = r * SBEt;
            CP = Math.Cos( PHI );
            SP = Math.Sin( PHI );
            AMINDIST = 10000000;// !WILL HAVE SHORTEST SUB TO RECEIVER DIST;
            SUM = 0.0;
            SUMW = 0.0;
            for(long I = 1; I <= NL; I++)
            {
                for(long J = 1; J <= NW; J++)
                {
                    YS = DL * (I - 1 - NL2);
                    XS = DW * (J - 1 - NW2) * SP;
                    ZS = DW * (J - 1 - NW2) * CP - H;// !FROM LOWERMOSTST TO UPPERMOST SUB IN A ROW
                    //		DIST=Math.Sqrt(Math.Pow((YS-YRC),2)+Math.Pow((XS-XRC),2)+Math.Pow(ZS,2));
                    DIST = Math.Sqrt( Math.Pow( (YS - YRC),2.0 ) + Math.Pow( (XS - XRC),2.0 ) + Math.Pow( ZS,2.0 ) );
                    CONTRIB = ATT(DIST, mag);
                    SUM = SUM + CONTRIB;
                    SUMW = SUMW + 1.0;
                    AMINDIST = minimum( AMINDIST,DIST );
                    //		if(DIST<AMINDIST)AMINDIST=DIST;
                }
            }

a100:
            //if(r < 0.001)
                //MessageBox.Show( "R IS SMALL!!!!" );

            RR = Math.Sqrt( Math.Pow( r,2.0 ) + Math.Pow( H,2.0 ) );
            corr = (SUM / SUMW) / (ATT(RR, mag));
            //corr = 1/SUMW;
        }

        double ATT(double r, double mag)
        {
            //   COMMON/ATTMED/AN1,RQ1,RSWITCH,AN2,RQ2,CC,RA,RB,DR111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111

            //RSWITCH = Math.Pow((double)10.0, (0.413 * mag - 1.204));
            //CC = CATT(RSWITCH, AN1, RQ1) / CATT(RSWITCH, AN2, RQ2);
            //RA = RSWITCH * 0.85;
            //RB = RSWITCH * 1.15;
            //DR = RB - RA;
            
            if(r <= RA)
                return (CATT( r,ATTPAR[ 1 ],ATTPAR[ 2 ] ));
            if(r >= RB)
                return (CC * CATT( r,ATTPAR[ 4 ],ATTPAR[ 5 ] ));
            if(r >= RA && r < RB)
                return (CATT( RA,ATTPAR[ 1 ],ATTPAR[ 2 ] ) + ((r - RA) / DR) * (CC * CATT( RB,ATTPAR[ 4 ],ATTPAR[ 5 ] ) - CATT( RA,ATTPAR[ 1 ],ATTPAR[ 2 ] )));
            return -1;
        }

        double CATT(double r,double an,double rq)
        {
            return (1.0 / (Math.Pow( Math.Pow( r,an ),2.0 ) * Math.Exp( r / rq )));//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        /// REFFFF long I0
        private void PROCVZ(ref long I0)
        {
            long ret;
            if(ISBR == 1)
            {
                ret = INPVZ( 2 );
                if(emexit == 1)
                    return;
                if(ret == 2)
                {
                    IGER = 1;
                    return;
                }
            } else
                SOURCV( ref I0 );
        }

        void SOURCV(ref long I0)
        {
            double[] X0 = new double[ 4 ],
                X1 = new double[ 4 ],
                X2 = new double[ 4 ],
                X3 = new double[ 4 ],
                X4 = new double[ 4 ],
                XC = new double[ 4 ];

            IGPM2 = 5;
            IORM2 = 5;
            IFLT = 0;
            long NIZ = NVS + 1;
            long kk;
            MRND( INMAX,ref I0 );
            SMAG = MGNT[ I0 ];//////////////ошибка
            NORMRND( ref S1,ref S3 );
            SA0 = Math.Pow( (double)10,(SMAG - CMAG + S1 * SDEVA) );
            if(SMAG < CMW1) { ALBYW = CLW1; }
            if(SMAG < CMW2 && SMAG >= CMW1) { ALBYW = CLW1 + (CLW2 - CLW1) * (SMAG - CMW1) / (CMW2 - CMW1); }
            if(SMAG >= CMW2) { ALBYW = CLW2; }
            SL0 = Math.Sqrt( SA0 * ALBYW );
            SW0 = SL0 / ALBYW;
            for(kk = 1; kk <= NIZ - 1; kk++)
            {
                XYP[ 1,kk ] = XP[ kk ];
                XYP[ 2,kk ] = YP[ kk ];
                XYP[ 3,kk ] = 0.0;
            }
            XYP[ 1,NIZ ] = 1.0 / Math.Pow( 10,3 );
            L = 1;
            long ret;
            ret = POGON( X0[ 1 ],X0[ 2 ],10,NIZ,ref L );
            if(ret == 10) { goto a10; }
            IGIP = 0;
a10:
            IGIP = IGIP + 1;
            if(IGIP >= IGPM2 + 1) { goto a60; }
a11:
            S1 = RAND();//!
            S2 = RAND();//!
            for(kk = 1; kk <= 2; kk++)
            {
                X0[ kk ] = RECS[ kk,1 ] + S1 * RECS[ kk,2 ] + S2 * RECS[ kk,3 ];
            }
            ret = POGON( X0[ 1 ],X0[ 2 ],11,NIZ,ref L );
            if(ret == 11)
                goto a11;
            ITEST = 0;
a50:
            ITEST = ITEST + 1;
            if(ITEST >= IORM2 + 1) { goto a10; }
            if(SMAG >= THR)
                SRCAZ = AZS + (1.0 - 2.0 * RAND()) * SAZ;
            else
                SRCAZ = 2.0 * PI * RAND();

            if(SMAG < THR1)
            {
                CO = 1.0 - 2.0 * RAND();
                if(CO >= 1.0) { DIP = 0.0; } else if(CO <= -1.0) { DIP = PI; } else { DIP = Math.Acos( CO ); }
            } else
            {
                P = RAND();
                P1 = RAND();
                if(P < PRT1) { DIP = DPS1 + (1 - 2.0 * P1) * DVS1; } else { DIP = DPS2 + (1 - 2.0 * P1) * DVS2; }
            }
            if(KDEP == 1)
            {
                RHS = SW0 * Math.Sin( DIP );
                H0U = DPTHU + (.5 + AKDIP) * RHS;
                if(HA[ 1 ] > H0U) { H0U = HA[ 1 ]; }
                H0L = DPTHL - (.5 - AKDIP) * RHS;
                if(HA[ 2 ] < H0L) { H0L = HA[ 2 ]; }
                if(H0U > H0L) { goto a50; }
            } else
            {
                H0U = HA[ 1 ];
                H0L = HA[ 2 ];
            }
            S1 = RAND();
            SH = H0U + S1 * (H0L - H0U);
            X0[ 3 ] = SH;
            SRCR( X0,ref X1,ref X2,ref X3,ref X4,ref XC );//заполняем X1,X2,X3,X4,XC
            if(KPNTR == 1)
            {								//X0,X1,SMAG,NVS,BR,*50,XP,YP
                if(AKAZ != -0.5 && AKDIP != -0.5)
                { ret = TSTEND( X0,X1,SMAG,NVS,BR,50,XP,YP ); if(ret == 50)goto a50; }
                if(AKDIP != -0.5 && AKAZ != 0.5)
                { ret = TSTEND( X0,X2,SMAG,NVS,BR,50,XP,YP ); if(ret == 50)goto a50; }
                if(AKDIP != 0.5 && AKAZ != 0.5)
                { ret = TSTEND( X0,X3,SMAG,NVS,BR,50,XP,YP ); if(ret == 50)goto a50; }
                if(AKDIP != 0.5 && AKAZ != -0.5)
                { ret = TSTEND( X0,X4,SMAG,NVS,BR,50,XP,YP ); if(ret == 50)goto a50; }
            }
            SROT = Math.Sin( SRCAZ );
            CROT = Math.Cos( SRCAZ );
            SPAR[ 1 ] = SMAG;
            SPAR[ 2 ] = SL0;
            SPAR[ 3 ] = SW0;
            SPAR[ 4 ] = SRCAZ;
            SPAR[ 5 ] = DIP;
            SPAR[ 6 ] = X1[ 1 ];
            SPAR[ 7 ] = X1[ 2 ];
            SPAR[ 8 ] = X1[ 3 ];
            SPAR[ 9 ] = XC[ 1 ];
            SPAR[ 10 ] = XC[ 2 ];
            SPAR[ 11 ] = XC[ 3 ];
            RPAR[ 1 ] = SL0;
            RPAR[ 2 ] = SW0;
            RPAR[ 3 ] = XC[ 3 ];
            RPAR[ 4 ] = DIP - PI / 2.0;
            RPAR[ 5 ] = 0.0;
            RPAR[ 6 ] = 0.0;
            goto a59;
a60:
            if(KFAIL == 0)
            {
                try
                {
                    //fls.Open(NAME6,CFile::modeWrite|CFile::typeText|CFile::modeCreate);
                    fls = new StreamWriter( NAME6,false,Encoding.GetEncoding( 1251 ) );
                }
                catch(Exception ex)
                {
                    MessageBox.Show( "ERROR: " + ex.Message );
                    return;
                }

                //fls.WriteLine( "NOT SUITABLE: INDZ, MAG, L, W, AZ,DIP, PHI1, LMD1, H1" );
                KFAIL = 1;
            }
            DEGEDCON( PHI0,AL0,AZ0,X1[ 1 ],X1[ 2 ],ref PHI1,ref AL1 );
            PHI1 = PHI1 * RAD;
            AL1 = AL1 * RAD;
            AZBRK = (SRCAZ + AZ0) * RAD;
            DIPBRK = DIP * RAD;
            fls.WriteLine( String.Format( "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t",IND,SMAG,SL0,SW0,AZBRK,DIPBRK,PHI1,AL1,SH ) );
            IFLT = 1;
            return;
a59:
            for(int Ia = 1; Ia <= 3; Ia++)
            {
                US1[ Ia ] = X1[ Ia ];
                US2[ Ia ] = X2[ Ia ];
                US3[ Ia ] = X3[ Ia ];
                US4[ Ia ] = X4[ Ia ];
            }
        }

        private long TSTEND(double[] XB,double[] XE,double SMAG,long NVS,double[] BR,long ss,double[] XP,double[] YP)
        {
            long ret = 0,Ien;
            NIZ = NVS + 1;
            for(long Ken = 1; Ken <= NIZ - 1; Ken++)
            {
                XYP[ 1,Ken ] = XP[ Ken ];
                XYP[ 2,Ken ] = YP[ Ken ];
                XYP[ 3,Ken ] = 0.0;
            }
            XYP[ 1,NIZ ] = 1.0 / Math.Pow( 10,3 );
            L = 1;
            ret = POGON( XE[ 1 ],XE[ 2 ],2,NIZ,ref L );
            if(ret == 2)
                goto a2;
            ret = POGON( XE[ 1 ],XE[ 2 ],2,NIZ,ref L );
            if(ret == 2)
                goto a2;
            return 0;
a2:
            ret = SNMB( 30,XB,XE );
            if(ret == 30)
                goto a30;
            if(In == 0)
                goto a10;
            for(Ien = 1; Ien <= In; Ien++)
            {
                if(SMAG <= BR[ ISD[ Ien ] ])
                    continue;
                else { return ss; }
            }
a10:
            return 0;
a30:
            return ss;
        }


        private long SNMB(long ss,double[] XB,double[] XE)
        {
            double U,V,U1,V1,t;
            double[] D = new double[ 3 ];
            D[ 1 ] = XE[ 1 ] - XB[ 1 ];
            D[ 2 ] = XE[ 2 ] - XB[ 2 ];
            long Isn = 1;
            for(Isn = 1; Isn <= NVS; Isn++)
            {
                XP[ Isn ] = XP[ Isn ] - XB[ 1 ];
                YP[ Isn ] = YP[ Isn ] - XB[ 2 ];
            }
            long Jsn = 1;
            for(Isn = 1; Isn <= NVS - 1; Isn++)
            {
                U = XP[ Isn ] * D[ 2 ] - YP[ Isn ] * D[ 1 ];
                V = XP[ Isn + 1 ] * D[ 2 ] - YP[ Isn + 1 ] * D[ 1 ];
                U1 = XP[ Isn ] * D[ 1 ] + YP[ Isn ] * D[ 2 ];
                V1 = XP[ Isn + 1 ] * D[ 1 ] + YP[ Isn + 1 ] * D[ 2 ];
                if(U >= 0.0 && V < 0.0)
                {
                    if(U1 < 0.0 && V1 < 0.0)
                        continue;
                    IS1[ Jsn ] = Isn;
                    Jsn = Jsn + 1;
                    if(U1 > 0.0 && V1 > 0.0) { goto a2; }
                }
            }
a2:
            In = Jsn - 1;
            long J1 = 1;
            for(Jsn = 1; Jsn <= In; Jsn++)
            {
                U = XP[ IS1[ Jsn ] ] - XP[ IS1[ Jsn ] + 1 ];
                V = YP[ IS1[ Jsn ] ] - YP[ IS1[ Jsn ] + 1 ];
                DET = D[ 1 ] * V - D[ 2 ] * U;
                if(Math.Abs( DET ) < 1.0e-12)
                    return ss;
                U1 = XP[ IS1[ Jsn ] ] * V - YP[ IS1[ Jsn ] ] * U;
                V1 = YP[ IS1[ Jsn ] ] * D[ 1 ] - XP[ IS1[ Jsn ] ] * D[ 2 ];
                t = U1 / DET;
                S = V1 / DET;
                if(t >= 0.0 && t <= 1.0 && S >= 0.0 && S <= 1.0)
                {
                    ISD[ J1 ] = IS1[ Jsn ];
                    J1 = J1 + 1;
                }
            }
            for(Isn = 1; Isn <= NVS; Isn++)
            {
                XP[ Isn ] = XP[ Isn ] + XB[ 1 ];
                YP[ Isn ] = YP[ Isn ] + XB[ 2 ];
            }
            In = J1 - 1;
            return 0;
        }

        private void SRCR(double[] X0,ref double[] X1,ref double[] X2,ref double[] X3,ref double[] X4,ref double[] XC)
        {
            double[] G1 = new double[ 4 ],
                G2 = new double[ 4 ];
            double VL,VW;

            double COSA = Math.Cos( SRCAZ );
            double SINA = Math.Sin( SRCAZ );
            double COSD = Math.Cos( DIP );
            G1[ 1 ] = COSA * COSD;
            G1[ 2 ] = -SINA * COSD;
            G1[ 3 ] = Math.Sin( DIP );
            G2[ 1 ] = SINA;
            G2[ 2 ] = COSA;
            G2[ 3 ] = 0.0;
            double UW = -AKDIP * SW0;
            double UL = -AKAZ * SL0;
            for(long ik = 1; ik <= 3; ik++)
            {
              VL = 0.5 * SL0 * G2[ ik ];
              VL = VL;
              VW = 0.5 * SW0 * G1[ ik ];
                
               // XC[ ik ] = X0[ ik ] + UW * G1[ ik ] + UL * G2[ ik ];
                XC[ik] = X0[ik] + UW * G1[ik] + UL * G2[ik];
                X1[ik] = XC[ik] - VW - VL + VL;
                X2[ik] = XC[ik] - VW + VL + VL;
                X3[ik] = XC[ik] + VW + VL + VL;
                X4[ik] = XC[ik] + VW - VL + VL;
            }
        }

        private void CLCRB3(long im3,double smg,double[] bsm3,double[] dst3,ref double r3m)
        {
            int k;
            for(k = 1; k <= im3 - 1; k++)
            {
                if(smg >= bsm3[ k ] && smg <= bsm3[ k + 1 ])
                {
                    r3m = (smg - bsm3[ k ]) / (bsm3[ k + 1 ] - bsm3[ k ]);
                    r3m = dst3[ k ] + r3m * (dst3[ k + 1 ] - dst3[ k ]);
                }
            }
            if(smg > bsm3[ im3 ])
                r3m = dst3[ im3 ];
        }

        private double MwToMl(double mw)
        {
            //return Math.Round(((-0.0295* mw* mw* mw) + (0.5014* mw* mw) + (-1.5317* mw) + 3.2564),1);
            return Math.Round(2 * (-0.0078 * mw * mw * mw * mw + 0.1758 * mw * mw * mw - 1.4755 * mw * mw + 6.7409 * mw - 9.4293)) * 0.5;
        }

        private void NORMRND(ref double S1,ref double S2)
        {
            double v1,v2,s1,x,y;
a1:
            x = RAND();//!
            y = RAND();//!
            v1 = 2.0 * x - 1;
            v2 = 2.0 * y - 1;
            s1 = v1 * v1 + v2 * v2;
            ///if(s1 >= 1.0 || s1 < 1.0e-20) { goto a1; }
            S1 = -Math.Sqrt( -2.0 * Math.Log( s1 ) / s1 ) * v1;
            S2 = -Math.Sqrt( -2.0 * Math.Log( s1 ) / s1 ) * v2;

            if(Double.IsNaN( S1 ) || Double.IsNaN( S2 ))
                NORMRND(ref S1,ref  S2 );
            //MessageBox.Show("NaN");
        }

        private void EXPRND(ref long IY,double SL,ref double X)
        {
a1:
            U = 1.0 - RAND();
            if(U < 1.0E-20)
                goto a1;
            X = -Math.Log( U ) / SL;
        }

        private long POGON(double x,double y,long ss,long kv,ref long l)
        {
            // По ИДЕЕ может быть не REF long L
            double A,B,B1;
            double sa;
            long N,J,I;
            if(kv == 1)
                goto a100;
            if(l == 1)
                goto a1;
            if(l == 2)
                goto a10;
a1:
            EPS = XYP[ 1,(long)kv ];
            kp = kv - 2;
            for(I = 1; I <= kp; I++)
            {
                for(J = 1; J <= 2; J++)
                {
                    if(Math.Abs( XYP[ J,I + 1 ] - XYP[ J,I ] ) < EPS) { XYP[ J,I + 1 ] += EPS + EPS; }
                }
                XYP[ 3,I ] = (XYP[ 1,I + 1 ] - XYP[ 1,I ]) / (XYP[ 2,I + 1 ] - XYP[ 2,I ]);
            }
            l = 2;//          !ОБХОД НАСТРОЙКИ
            return 0;
a10:
            N = 0;
            B = y - XYP[ 2,1 ];
            B1 = 0.0;
            for(I = 1; I <= kp; I++)
            {
                if(B1 == 1)
                {
                    B1 = 0.0;
                    goto a14;
                }
                A = B;
                B = y - XYP[ 2,I + 1 ];
                sa = A * B;
                if(sa < 0) goto a11;
                else if(sa == 0) goto a13;
                else continue;
a11:
                XX = A * XYP[ 3,I ] + XYP[ 1,I ];
a12:
                if(XX < x) { N += 1; };
                continue;
a13:
                if(B == 0.0)
                {
                    B1 = 1.0;
                    continue;
                }
a14:
                XX = XYP[ 1,I ];
                goto a12;
            }

            if((N % 2) == 0)
                return ss;
            else
                return 0;
a100:
            R = Math.Pow( (X - XYP[ 1,1 ]),2.0 ) + Math.Pow( (Y - XYP[ 2,1 ]),2.0 );
            return 0;
        }

        private void MRND(long N,ref long I0)
        {
            double S = RAND();//0

            for(long I = 1; I <= N; I++)
            {
                if(S < GRF[ I ])
                    continue;
                I0 = I - 1;
                goto a2;
            }
            I0 = N;
a2:
            if(I0 == 0)
                I0 = 1;
        }

        private double RAND()//(ref long IY)
        {
            /*
            double HALFM = random.NextDouble();

            if(M2 == 0) 
                M2 = Convert.ToInt64( HALFM * 100.0 );

            IA = 8 * (long)(HALFM * Math.Atan( 1.0 ) / 8.0) + 5;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            IC = 2 * (long)(HALFM * (0.5 - Math.Sqrt( 3.0 ) / 6.0)) + 1;
            s = 0.5 / HALFM;

            IY = IY * IA;
            IY = IY + IC;

            return (double)(IY * s);*/


            long M;
            double HALFM;
            if(M2 != 0)
                goto a20;
            M = 1;
a10:
            M2 = M;
            M = ITWO * M2;
            if(M > M2)
                goto a10;
            HALFM = M2;
            IA = 8 * (long)(HALFM * Math.Atan( 1.0 ) / 8.0) + 5;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            IC = 2 * (long)(HALFM * (0.5 - Math.Sqrt( 3.0 ) / 6.0)) + 1;
            MIC = (M2 - IC) + M2;
            s = 0.5 / HALFM;
a20:
            IY = IY * IA;
            if(IY > MIC)
                IY = (IY - M2) - M2;
            IY = IY + IC;
            if(IY / 2 > M2)
                IY = (IY - M2) - M2;
            if(IY < 0)
                IY = (IY + M2) + M2;
            return (double)(IY * s);
        }

        private long INPVZ(long ss) //выводит в файлы *_GRP и *_OUT информацию по зонам, чтение данных из базы,
        {
            string aa = "";
            long J1,J2;
            long Jvz,Kvz;
            EPS = 1.0e-3;
            ZONE = "ZONA";
            outFile.WriteLine( "ZONA " + IND.ToString() + "" );
            grp.WriteLine( "ZONA " + IND.ToString() + "" );
            grp.WriteLine( "MAG,GRFO,GRFM (KUMUL) " );

            NVS = Convert.ToInt64( rdomDT.Rows[ position_rdom ][ "nvs" ] );
            KOBH = Convert.ToInt64( rdomDT.Rows[ position_rdom ][ "kobh" ] );
            outFile.WriteLine( String.Format( "NVS, KOBH {0} {1}",NVS,KOBH ) );

            if(NVS > NM1)
            {
                wrng_out.WriteLine( "Зона " + IND.ToString() );
                wrng_out.WriteLine( "Число вершин полигона больше допустимого " + NM1.ToString() );
                EmExit( "Ошибка в субмодуле INPVZ" );
                return -1;
            }

            KBR = Convert.ToInt64( rdomDT.Rows[ position_rdom ][ "kbr" ] );
            BRA = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "bra" ] );

            outFile.WriteLine( String.Format( "KBR, BRA {0} {1}",KBR,BRA ) );
            DataTable rbzDT = new DataTable( "rbzDT" );
            rbzDT = FillTable( "select * from Вершины_доменов where ind=" + IND + " ORDER BY n_verh ASC" );

            Jvz = 1;
            for(int i = 0; i < rbzDT.Rows.Count; i++)
            {
                XPU[ Jvz ] = Convert.ToDouble( rbzDT.Rows[ i ][ "pf" ] );
                YPU[ Jvz ] = Convert.ToDouble( rbzDT.Rows[ i ][ "pl" ] );

                if(KBR != 1)
                    BRU[ Jvz ] = Convert.ToDouble( rbzDT.Rows[ i ][ "brm" ] );

                Jvz++;
            }
            rbzDT.Clear();

            for(Jvz = 1; Jvz <= NVS; Jvz++)
            {
                if(KMAG == 0)
                {
                    CLCRB3( IMW,BRU[ Jvz ],OMLH,OMW,ref X );
                    BRU[ Jvz ] = X;
                }
                if(KOBH == 1)
                {
                    XP[ Jvz ] = XPU[ Jvz ];
                    YP[ Jvz ] = YPU[ Jvz ];
                    BR[ Jvz ] = BRU[ Jvz ];
                }
            }

            if(KOBH == 0)
            {
                for(Jvz = 1; Jvz <= NVS; Jvz++)
                {
                    J1 = NVS - Jvz + 1;
                    J2 = NVS - Jvz;
                    XP[ Jvz ] = XPU[ J1 ];
                    YP[ Jvz ] = YPU[ J1 ];
                    if(J2 != 0)
                        BR[ Jvz ] = BRU[ J2 ];
                }
                BR[ NVS ] = BR[ 1 ];
            }

            for(Jvz = 1; Jvz <= NVS; Jvz++)
            {
                X1 = XP[ Jvz ] / RAD;
                Y1 = YP[ Jvz ] / RAD;
                GEDECCON( PHI0,AL0,AZ0,X1,Y1,ref X,ref Y );
                XP[ Jvz ] = X;
                YP[ Jvz ] = Y;
            }

            INMAX = Convert.ToInt64( rdomDT.Rows[ position_rdom ][ "inmax" ] );
            KUMUL = Convert.ToInt64( rdomDT.Rows[ position_rdom ][ "kumul" ] );

            SZON = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "szon" ] );

            outFile.WriteLine( String.Format( "INMAX, KUMUL, SZON {0}\t{1}\t{2}",INMAX,KUMUL,SZON ) );
            rbzDT = FillTable( "select * from Сейсм_режим_доменов where ind=" + IND + " ORDER BY n_magn ASC" );

            Kvz = 1;
            for(int i = 0; i < rbzDT.Rows.Count; i++)
            {
                MGN[ Kvz ] = Convert.ToDouble( rbzDT.Rows[ i ][ "mgnt" ] );
                GR[ Kvz ] = Convert.ToDouble( rbzDT.Rows[ i ][ "grf" ] );

                Kvz++;
            }

            FIRSTMAG = MGN[1];
            rbzDT.Clear();



            if(KUMUL == 1)
            {
                for(Kvz = 2; Kvz <= INMAX; Kvz++)
                {
                    if(GR[ Kvz ] > GR[ Kvz - 1 ])
                    {
                        wrng_out.WriteLine( "Зона " + IND.ToString() );
                        wrng_out.WriteLine( "KUMUL=1, но график НЕКУМУЛЯТИВНЫЙ!!!" );
                        EmExit( "Ошибка в субмодуле INPVZ" );
                        return -1;
                    }
                }
            }

            for(long k = 1; k <= INMAX; k++)
            {
                if(KMAG == 0)
                {
                    CLCRB3( IMW,MGN[ k ],OMLH,OMW,ref X );
                    MGN[ k ] = X;
                }
            }

            if(KUMUL == 0)
            {

              if (fastCalc == 1)
                {
                    GR2[INMAX + 1] = 0.0;
                    GR2[INMAX] = GR[1];
                    for (Kvz = INMAX - 1; Kvz >= 1; Kvz--)
                        GR2[Kvz] = GR[1] + GR2[Kvz + 1];
                }
              else
                {
                    GR[ INMAX + 1 ] = 0.0;
                    for (Kvz = INMAX; Kvz >= 1; Kvz--)
                        GR[ Kvz ] = GR[ Kvz + 1 ] + GR[ Kvz ];
                }

            }

            if (fastCalc==1)
            {
                KOEFF[0] = 0;
                for (int i_koeff = 1; i_koeff <= INMAX; i_koeff++)
                {
                    KOEFF[i_koeff] = GR[i_koeff] / GR[1];
                }
            }



                HA[1] = Convert.ToDouble(rdomDT.Rows[position_rdom]["h1"]);
            HA[ 2 ] = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "h2" ] );

            KDEP = Convert.ToInt64( rdomDT.Rows[ position_rdom ][ "kdep" ] );
            DPTHU = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "dpthu" ] );
            DPTHL = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "dpthl" ] );

            AKDIP = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "akdip" ] );
            AKAZ = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "akaz" ] );
            DEVL = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "devl" ] );
            DEVC = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "devc" ] );

            KSTIC = Convert.ToInt64( rdomDT.Rows[ position_rdom ][ "kstic" ] );
            THR = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "thr" ] );
            AZS = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "azs" ] );
            SAZ = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "saz" ] );

            if(KMAG == 0)
            {
                CLCRB3( IMW,THR,OMLH,OMW,ref X );
                THR = X;
            }

            AZS = AZS / RAD - AZ0;
            SAZ = SAZ / RAD;

            THR1 = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "thr1" ] );
            PRT1 = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "p1" ] );
            PRT2 = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "p2" ] );

            if(KMAG == 0)
            {
                CLCRB3( IMW,THR1,OMLH,OMW,ref X );
                THR1 = X;
            }

            DPS1 = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "dips1" ] );
            DVS1 = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "sdip1" ] );
            DPS2 = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "dips2" ] );
            DVS2 = Convert.ToDouble( rdomDT.Rows[ position_rdom ][ "sdip2" ] );

            X = PRT1 + PRT2;

            if(X < EPS)
            {
                EmExit( "Ошибка в субмодуле INPFZ Зона ВОЗ=" + IND.ToString() + " [PRT1+PRT2=0]" );
                return -1;
            }// STOP 'PRT1+PRT2=0'

            PRT1 = PRT1 / X;
            PRT2 = PRT2 / X;
            DPS1 = DPS1 / RAD;
            DVS1 = DVS1 / RAD;
            DPS2 = DPS2 / RAD;
            DVS2 = DVS2 / RAD;

            if(KMAG == 0)
            {
                CLCRB3( IMW,THR1,OMLH,OMW,ref X );
                THR1 = X;
            }

            KPNTR = Convert.ToInt64( rdomDT.Rows[ position_rdom ][ "kpntr" ] );
            outFile.WriteLine( "KPNTR=" + KPNTR );

            if(KDEP == 1)
            {
                for(Jvz = 1; Jvz <= INMAX; Jvz++)
                {
                    SMAG = MGN[ Jvz ];
                    SQ0 = Math.Pow( (double)10.0,(SMAG - CMAG) );
                    if(SMAG < CMW1)
                        ALBYW = CLW1;
                    if(SMAG < CMW2 && SMAG >= CMW1)
                        ALBYW = CLW1 + (CLW2 - CLW1) * (SMAG - CMW1) / (CMW2 - CMW1);
                    if(SMAG >= CMW2)
                        ALBYW = CLW2;
                    SL0 = Math.Sqrt( SQ0 * ALBYW );
                    SW0 = SQ0 / SL0;
                    if(SMAG >= THR1)
                    {
                        if(PRT1 != 0.0)
                        {
                            DIP = DPS1;
                            RHS = SW0 * Math.Sin( DIP );
                            H0U1 = DPTHU + (0.5 + AKDIP) * RHS;
                            if(HA[ 1 ] > H0U1)
                                H0U1 = HA[ 1 ];
                            H0L1 = DPTHL - (0.5 - AKDIP) * RHS;
                            if(HA[ 2 ] < H0L1)
                                H0L1 = HA[ 2 ];
                            if(H0U1 > H0L1)
                            {
                                wrng_out.WriteLine( "Зона " + IND );
                                wrng_out.WriteLine( "Условия на глубины противоречивы" );
                                DIP1 = DIP * RAD;
                                wrng_out.WriteLine( "MAG\tW\tDPS1\tH1\tH2\tDPT\tHU\tDPTHL" );
                                str = String.Format( "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t",SMAG,SW0,DIP1,HA[ 1 ],HA[ 2 ],DPTHU,DPTHL );
                                wrng_out.WriteLine( str );
                                wrng_out.WriteLine( String.Format( "H0U1, H0L1 {0}\t{1}",H0U1,H0L1 ) );
                                //						99           FORMAT(1X,9F8.2)
                            }
                        }

                        if(PRT2 != 0.0)
                        {
                            DIP = DPS2;
                            RHS = SW0 * Math.Sin( DIP );
                            H0U1 = DPTHU + (.5 + AKDIP) * RHS;
                            if(HA[ 1 ] > H0U1)
                                H0U1 = HA[ 1 ];
                            H0L1 = DPTHL - (.5 - AKDIP) * RHS;
                            if(HA[ 2 ] < H0L1)
                                H0L1 = HA[ 2 ];
                            if(H0U1 > H0L1)
                            {
                                wrng_out.WriteLine( "Зона " + IND );
                                wrng_out.WriteLine( "Условия на глубины противоречивы" );
                                DIP1 = DIP * RAD;
                                wrng_out.WriteLine( "MAG\tW\tDPS1\tH1\tH2\tDPT\tHU\tDPTHL" );
                                str = String.Format( "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t",SMAG,SW0,DIP1,HA[ 1 ],HA[ 2 ],DPTHU,DPTHL );
                                wrng_out.WriteLine( str );
                                wrng_out.WriteLine( String.Format( "H0U1, H0L1 {0}\t{1}",H0U1,H0L1 ) );
                            }
                        }
                    }
                }
            }

            RMIN = 1.0e+6;

            for(Jvz = 1; Jvz <= NETPNT; Jvz++)
            {
                DSTPLG( XNET[ Jvz ],YNET[ Jvz ],XP,YP,NVS,ref DMIN );
                if(DMIN < RMIN)
                    RMIN = DMIN;
            }

            RMIN = Math.Sqrt( Math.Pow( RMIN,2.0 ) + Math.Pow( HA[ 1 ],2.0 ) );
            K0 = 0;

            if(RMIN != 0.0)
            {
                for(Jvz = 1; Jvz <= INMAX; Jvz++)
                {
                    CLCRB3( IM3,MGN[ Jvz ],BSM3,DST3,ref R3M );
                    if(R3M < RMIN)
                        K0 = K0 + 1;
                }
            }

            if(K0 == INMAX)
            {
                outFile.WriteLine( String.Format( "ZONE {0} EXCLUDED",IND ) );
                outFile.WriteLine( " " );
                return ss;
            } else
            {
                INMAX = INMAX - K0;
                for(Jvz = 1; Jvz <= INMAX; Jvz++)
                {
                    MGNT[ Jvz ] = MGN[ Jvz + K0 ];

                    if (fastCalc == 1)
                        GRF[ Jvz ] = GR2[ Jvz + K0 ] * SZON;
                    else
                        GRF[Jvz] = GR[Jvz + K0] * SZON;
                }
            }

            outFile.WriteLine( String.Format( "MINMAG,MAXMAG {0} {1}",MGNT[ 1 ],MGNT[ INMAX ] ) );
            SLD = GRF[ 1 ];
            for(Kvz = 1; Kvz <= INMAX; Kvz++)
            {
                if (fastCalc ==1)
                    GR2[ Kvz ] = GRF[ Kvz ] / SZON;
                else
                    GR[Kvz] = GRF[Kvz] / SZON;

                GRF[ Kvz ] = GRF[ Kvz ] / SLD;
            }
            RECZON( XP,NVS );
            return 0;
        }

        private void DSTPRG(double x,double y,double z,double[] c1,double[] c2,double[] c3,double[] c4,ref double d)
        {
            long aa = 1;
            double D1 = 0.0;
            double[] U = new double[ 4 ],V = new double[ 4 ];
            double[ , ] W = new double[ 4,4 ];
            U[ 1 ] = x;
            U[ 2 ] = y;
            U[ 3 ] = z;
            long Io,J;
            for(Io = 1; Io <= 3; Io++)
            {
                for(J = 1; J <= 3; J++) { W[ Io,J ] = 0; }
            }
            for(Io = 1; Io <= 3; Io++)
            {
                W[ 1,1 ] = W[ 1,1 ] + Math.Pow( (c2[ Io ] - c1[ Io ]),2.0 );
                W[ 1,2 ] = W[ 1,2 ] + (c2[ Io ] - c1[ Io ]) * (c4[ Io ] - c1[ Io ]);
                W[ 1,3 ] = W[ 1,3 ] + (c2[ Io ] - c1[ Io ]) * (U[ Io ] - c1[ Io ]);
                W[ 2,2 ] = W[ 2,2 ] + Math.Pow( (c4[ Io ] - c1[ Io ]),2.0 );
                W[ 2,3 ] = W[ 2,3 ] + (c4[ Io ] - c1[ Io ]) * (U[ Io ] - c1[ Io ]);
                W[ 3,3 ] = W[ 3,3 ] + (U[ Io ] - c1[ Io ]) * (U[ Io ] - c1[ Io ]);
            }
            W[ 2,1 ] = W[ 1,2 ];
            W[ 3,1 ] = W[ 1,3 ];
            W[ 3,2 ] = W[ 2,3 ];
            DET = W[ 1,1 ] * W[ 2,2 ] - Math.Pow( W[ 1,2 ],2.0 );
            V[ 1 ] = (W[ 1,3 ] * W[ 2,2 ] - W[ 1,2 ] * W[ 2,3 ]) / DET;
            V[ 2 ] = (W[ 2,3 ] * W[ 1,1 ] - W[ 1,2 ] * W[ 1,3 ]) / DET;
            V[ 3 ] = -1.0;
            if(((V[ 1 ] >= 0.0 && V[ 1 ] <= 1.0) && V[ 2 ] >= 0.0) && V[ 2 ] <= 1.0)
            {
                d = 0.0;
                for(Io = 1; Io <= 3; Io++)
                {
                    for(J = 1; J <= 3; J++) { d = d + W[ Io,J ] * V[ Io ] * V[ J ]; }
                }
                D1 = 1.0e-7;
                if(d < D1) { d = 0.0; }
                d = Math.Sqrt( d );
                return;
            } else
            {
                d = 1.0e+6;//
                aa = 3;
                DSTL3( aa,c1,c2,U,ref D1 );
                if(D1 < d)
                    d = D1;
                DSTL3( aa,c2,c3,U,ref D1 );
                if(D1 < d)
                    d = D1;
                DSTL3( aa,c3,c4,U,ref D1 );
                if(D1 < d)
                    d = D1;
                DSTL3( aa,c1,c4,U,ref D1 );
                if (D1 < d)
                    d = D1;
            }
        }

        private void DSTL3(long N,double[] A,double[] B,double[] X,ref double rm)
        {
            double p1 = 0.0,p2 = 0.0,t,D1;
            double[] Y = new double[ 4 ];
            long Is;
            D1 = 1.0e-7;
            IPROD3( N,X,A,B,A,ref p1 );
            IPROD3( N,B,A,B,A,ref p2 );
            t = p1 / p2;
            if(t <= 0.0)
            {
                IPROD3( N,X,A,X,A,ref p1 );
                if(p1 < D1) { p1 = 0.0; }
                rm = Math.Sqrt( p1 );
            } else if(t >= 1.0)
            {
                IPROD3( N,X,B,X,B,ref p1 );
                if(p1 < D1) { p1 = 0.0; }
                rm = Math.Sqrt( p1 );
            } else
            {
                for(Is = 1; Is <= 3; Is++) { Y[ Is ] = A[ Is ] + t * (B[ Is ] - A[ Is ]); }
                IPROD3( N,X,Y,X,Y,ref rm );
                if(rm < D1) { rm = 0.0; }
                rm = Math.Sqrt( rm );
            }
        }

        private void IPROD3(long N,double[] A,double[] U,double[] B,double[] V,ref double p)
        {
            p = 0.0;
            long Ia;
            for(Ia = 1; Ia <= N; Ia++)
                p += (A[ Ia ] - U[ Ia ]) * (B[ Ia ] - V[ Ia ]);
        }

        private void PROCFZ(ref long I0)
        {
            long ret;
            if(ISBR == 1)
            {
                ret = INPFZ( 2 );
                if(emexit == 1)
                    return;
                if(ret == 2)
                { IGER = 1; return; }
            } else
                SOURCF( ref I0 );
        }

        private void DSTPLG(double x,double y,double[] XP,double[] YP,long nv,ref double DMIn)
        {
            double[] A = new double[ 3 ],B = new double[ 3 ],U = new double[ 3 ];
            long Idst;
            U[ 1 ] = x;
            U[ 2 ] = y;
            for(Idst = 1; Idst <= nv; Idst++)
            {
                XYP[ 1,Idst ] = XP[ Idst ];
                XYP[ 2,Idst ] = YP[ Idst ];
                XYP[ 3,Idst ] = 0.0;
            }
            long NV1 = nv + 1;
            XYP[ 1,NV1 ] = 1.0E-3;
            L = 1;
            long ret,Js;
            ret = POGON( x,y,11,NV1,ref L );
            if(ret != 11)
            {
                ret = POGON( x,y,11,NV1,ref L );
                if(ret != 11)
                {
                    DMIn = 0.0;
                    return;
                }
            }

            if(ret == 11)
                goto a11;
            ret = POGON( x,y,11,NV1,ref L );
            if(ret == 11)
                goto a11;
            DMIn = 0.0;
            return;
a11:
            DMIn = 10000000.0;//10000
            for(Js = 1; Js <= nv - 1; Js++)
            {
                A[ 1 ] = XYP[ 1,Js ];
                A[ 2 ] = XYP[ 2,Js ];
                B[ 1 ] = XYP[ 1,Js + 1 ];
                B[ 2 ] = XYP[ 2,Js + 1 ];

                DSTL( A,B,U,ref RM );

                if(RM < DMIn)
                    DMIn = RM;
            }
        }

        private void DSTL(double[] A,double[] B,double[] X,ref double Rm)
        {
            double[] Y = new double[ 3 ];
            double p1 = 0.0,p2 = 0.0;
            IPROD( X,A,B,A,ref p1 );
            IPROD( B,A,B,A,ref p2 );
            double t = p1 / p2;
            if(t <= 0.0)
            {
                IPROD( X,A,X,A,ref p1 );
                Rm = Math.Sqrt( p1 );
            } else if(t >= 1.0)
            {
                IPROD( X,B,X,B,ref p1 );
                Rm = Math.Sqrt( p1 );
            } else
            {
                Y[ 1 ] = A[ 1 ] + t * (B[ 1 ] - A[ 1 ]);
                Y[ 2 ] = A[ 2 ] + t * (B[ 2 ] - A[ 2 ]);
                IPROD( X,Y,X,Y,ref Rm );
                Rm = Math.Sqrt( Rm );
            }
        }

        private void IPROD(double[] A,double[] U,double[] B,double[] V,ref double p)
        {
            p = (A[ 1 ] - U[ 1 ]) * (B[ 1 ] - V[ 1 ]) + (A[ 2 ] - U[ 2 ]) * (B[ 2 ] - V[ 2 ]);
        }

        private void RECZON(double[] XP,long NVS)
        {
            double[] A0 = new double[ 3 ],
                F1 = new double[ 3 ],
                F2 = new double[ 3 ];
            RECTT( XP,NVS,ref A0,ref F1,ref F2,0.0,0.0 );
            for(long Jq = 1; Jq <= 2; Jq++)
            {
                RECS[ Jq,1 ] = A0[ Jq ];
                RECS[ Jq,2 ] = F1[ Jq ];
                RECS[ Jq,3 ] = F2[ Jq ];
            }
        }

        private void RECTT(double[] XP,long NVS,ref double[] A0,ref double[] F1,ref double[] F2,double FL1,double FL2)
        {
            double RMAX = 0.0;
            long IMAX;
            for(long Irt = 1; Irt <= NVS - 1; Irt++)
            {
                X = XP[ Irt + 1 ] - XP[ Irt ];
                Y = YP[ Irt + 1 ] - YP[ Irt ];
                R = Math.Sqrt( X * X + Y * Y );
                if(R > RMAX)
                {
                    RMAX = R;
                    IMAX = Irt;
                    F1[ 1 ] = X / R;
                    F1[ 2 ] = Y / R;
                }
                F2[ 1 ] = -F1[ 2 ];
                F2[ 2 ] = F1[ 1 ];
            }
            double XMIN = 10000.0;
            double YMIN = 10000.0;
            double XMAX = -10000.0;
            double YMAX = -10000.0;
            for(long Irt = 1; Irt <= NVS - 1; Irt++)
            {
                X = XP[ Irt ] * F1[ 1 ] + YP[ Irt ] * F1[ 2 ];
                Y = XP[ Irt ] * F2[ 1 ] + YP[ Irt ] * F2[ 2 ];
                if(X > XMAX) { XMAX = X; }
                if(Y > YMAX) { YMAX = Y; }
                if(X < XMIN) { XMIN = X; }
                if(Y < YMIN) { YMIN = Y; }
            }
            for(long Irt = 1; Irt <= 2; Irt++)
            {
                A0[ Irt ] = XMIN * F1[ Irt ] + YMIN * F2[ Irt ];
                FL1 = XMAX - XMIN;
                FL2 = YMAX - YMIN;
                F1[ Irt ] = F1[ Irt ] * FL1;
                F2[ Irt ] = F2[ Irt ] * FL2;
            }
        }

        private void SOURCF(ref long I0)
        {
            double[] X0 = new double[ 4 ],
                        X1 = new double[ 4 ],
                        X2 = new double[ 4 ],
                        X3 = new double[ 4 ],
                        X4 = new double[ 4 ],
                        XC = new double[ 4 ];
            long k,k2 = 0,k1,k3,iss;
            PCOV = 0.5;
            IORM1 = 5;
            IFLT = 0;
            long NIZ = NVS + 1;
            MRND( INMAX,ref I0 );
            SMAG = MGNT[ I0 ];
            SL0 = SL[ I0 ];
            SW0 = SW[ I0 ];
            //C make rand coeff for SL0 and SW0
            NORMRND( ref S1,ref S3 );
            CSA0 = Math.Pow( (double)10.0,(0.5 * S1 * SDEVA) );//!coeff to L and W
            if(KPNTR >= 2)// !ukladka est!
            {
                SW0 = SW0 * Math.Pow( CSA0,2.0 );
            } else// !ukladki net, vse kak dlya AREA-ZONE
            {
                SW0 = SW0 * CSA0;
                SL0 = SL0 * CSA0;
            }
            k1 = k = NSEG[ I0 ];

            if(KPNTR == 4)
            {
                k2 = Convert.ToInt64( RAND() ) * k;
                if(k2 < k) { k2 = k2 + 1; }
            } else if(KPNTR >= 2)
            {
                if(k <= 100)
                {
                    if(L0[ I0 ] == 0)
                    {
                        for(iss = 1; iss <= k; iss++) { ISOS[ I0,iss ] = iss; }
                        k1 = k;
                    } else { k1 = L0[ I0 ]; }
                    k2 = (long)(RAND() * k1);
                    if(k2 < k1) { k2 = k2 + 1; }
                    k3 = k2;
                    k2 = ISOS[ I0,k2 ];
                    if(k1 > 1)
                    {
                        for(iss = 1; iss <= k1 - 1; iss++)
                        {
                            if(iss >= k3) { ISOS[ I0,iss ] = ISOS[ I0,iss + 1 ]; }
                        }
                    }
                    L0[ I0 ] = k1 - 1;
                } else
                {
                    k2 = (long)(RAND() * k);
                    if(k2 < k1) { k2 = k2 + 1; }
                };
            }

            ED[ 1 ] = ER[ 2 ];
            ED[ 2 ] = -ER[ 1 ];
            KTMP = 0;
a62:
            KTMP = KTMP + 1;
            if(KTMP > IORM1)
                goto a60;
            if(SMAG < THR1)
            {
                CO = 1.0 - (2.0 * RAND());
                if(CO >= 1.0)
                {
                    DIP = 0.0;
                } else if(CO <= -1.0)
                {
                    DIP = PI;
                } else { DIP = Math.Acos( CO ); }
            } else
            {
                P = RAND();
                P1 = RAND();
                if(P < PRT1) { DIP = DPS1 + (1 - 2.0 * P1) * DVS1; } else { DIP = DPS2 + (1 - 2.0 * P1) * DVS2; }
            }
            CDPF = Math.Cos( DIPF );///(-1.7271901609191);///(-0.57897504433898);//-1.7271901609191
            SDPF = Math.Sin( DIPF );

            if(SDPF == 0.0)
            {
                wrng_out.WriteLine( "Разлом горизонтален, N=" + IND );
                EmExit( "Ошибка в субмодуле SOURCF" );
                return;
            }
            CTDPF = CDPF / SDPF;
            if(KDEP == 1)
            {
                RHS = SW0 * Math.Sin( DIP );
                H0U = DPTHU + (.5 + AKDIP) * RHS;
                if(HA[ 1 ] > H0U)
                    H0U = HA[ 1 ];
                H0L = DPTHL - (.5 - AKDIP) * RHS;
                if(HA[ 2 ] < H0L)
                    H0L = HA[ 2 ];
                if(H0U > H0L) { goto a62; }
            } else
            {
                H0U = HA[ 1 ];
                H0L = HA[ 2 ];
            }
            S1 = RAND();
            SH = H0U + S1 * (H0L - H0U);
            X0[ 3 ] = SH;
            if(KPNTR == 0 || KPNTR == 1)
            {
                S1 = RAND();
                for(iss = 1; iss <= 2; iss++)
                {
                    X0[ iss ] = XB[ I0,iss ] + S1 * FL[ I0 ] * ER[ iss ] + (SH - HA[ 1 ]) * CTDPF * ED[ iss ];
                }
            } else if(KPNTR == 2)
            {
                for(iss = 1; iss <= 2; iss++)
                {
                    X0[ iss ] = XB[ I0,iss ] + (Convert.ToDouble( k2 ) - 0.5) * SL0 * ER[ iss ] + (SH - HA[ 1 ]) * CTDPF * ED[ iss ];
                }
            } else
            {
                FL0 = FL[ 1 ];
                C1 = FL[ 2 ];
                CS = FL[ 3 ];
                FL1 = k * SL0;
                if(FL1 >= FL0)
                {
                    for(iss = 1; iss <= 2; iss++) { X0[ iss ] = XB[ I0,iss ] + (k2 - .5) * SL0 * ER[ iss ] + (SH - HA[ 1 ]) * CTDPF * ED[ iss ] - ER[ iss ] * (FL1 - FL0) * C1 / CS; }
                } else
                {
                    for(iss = 1; iss <= 2; iss++) { X0[ iss ] = XB[ I0,iss ] + (k2 - .5) * (FL0 / k) * ER[ iss ] + (SH - HA[ 1 ]) * CTDPF * ED[ iss ] + ER[ iss ] * (1.0 - 2.0 * RAND()) * (FL0 / k - SL0) * PCOV; }
                }
            }
            SRCAZ = AZS;
            AKAZ = 0.0;
            SRCR( X0,ref X1,ref X2,ref X3,ref X4,ref XC );//заполняем X1,X2,X3,X4,XC
            SROT = Math.Sin( SRCAZ );
            CROT = Math.Cos( SRCAZ );
            SPAR[ 1 ] = SMAG;
            SPAR[ 2 ] = SL0;
            SPAR[ 3 ] = SW0;
            SPAR[ 4 ] = SRCAZ;
            SPAR[ 5 ] = DIP;
            SPAR[ 6 ] = X1[ 1 ];
            SPAR[ 7 ] = X1[ 2 ];
            SPAR[ 8 ] = X1[ 3 ];
            SPAR[ 9 ] = XC[ 1 ];
            SPAR[ 10 ] = XC[ 2 ];
            SPAR[ 11 ] = XC[ 3 ];
            RPAR[ 1 ] = SL0;
            RPAR[ 2 ] = SW0;
            RPAR[ 3 ] = XC[ 3 ];
            RPAR[ 4 ] = DIP - PI / 2.0;
            RPAR[ 5 ] = 0.0;
            RPAR[ 6 ] = 0.0;
            goto a59;
a60:
            if(KFAIL == 0)
            {
                try
                {
                    //fls.Open(NAME6,CFile::modeWrite|CFile::typeText|CFile::modeCreate);
                    fls = new StreamWriter( NAME6,false,Encoding.GetEncoding( 1251 ) );
                }
                catch(Exception ex)
                {
                    MessageBox.Show( "ERROR: " + ex.Message );
                    return;
                }
               // fls.WriteLine( "NOT SUITABLE: INDZ, MAG, L, W, AZ,DIP, PHI1, LMD1, H1" );
                KFAIL = 1;
            }
            DEGEDCON( PHI0,AL0,AZ0,X1[ 1 ],X1[ 2 ],ref PHI1,ref AL1 );
            PHI1 = PHI1 * RAD;
            AL1 = AL1 * RAD;
            AZBRK = (SRCAZ + AZ0) * RAD;
            DIPBRK = DIP * RAD;
            fls.WriteLine( String.Format( "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t",IND,SMAG,SL0,SW0,AZBRK,DIPBRK,PHI1,AL1,SH ) );
            IFLT = 1;
a59:
            for(iss = 1; iss <= 3; iss++)
            {
                US1[ iss ] = X1[ iss ];
                US2[ iss ] = X2[ iss ];
                US3[ iss ] = X3[ iss ];
                US4[ iss ] = X4[ iss ];
            }
        }

        private long INPFZ(long ss)// линеаменты
        {
            string aa = "";
            int i,j;
            long kj;
            ZONE = "ZONA";
            EPS = 1.0E-3;

            grp.WriteLine( "ZONE " + IND );
            grp.WriteLine( "MAG,GRFO,GRFM (KUMUL)" );

            NVS = Convert.ToInt64( rlinDT.Rows[ li ][ "nvs" ] );
            KOBH = Convert.ToInt64( rlinDT.Rows[ li ][ "kobh" ] );
            DIPF = Convert.ToInt64( rlinDT.Rows[ li ][ "dipf" ] );
            INMAX = Convert.ToInt64( rlinDT.Rows[ li ][ "inmax" ] );
            KUMUL = Convert.ToInt64( rlinDT.Rows[ li ][ "kumul" ] );
            SZON = Convert.ToInt64( rlinDT.Rows[ li ][ "szon" ] );

            DataTable rbzDT = new DataTable( "rbzDT" );
            rbzDT = FillTable( "select * from Вершины_линеаментов where ind=" + IND + " ORDER BY n_verh ASC" );

            j = 1;
            for(i = 0; i < rbzDT.Rows.Count; i++)
            {
                XP[ j ] = Convert.ToDouble( rbzDT.Rows[ i ][ "pf" ] );
                YP[ j ] = Convert.ToDouble( rbzDT.Rows[ i ][ "pl" ] );
                PRT[ j ] = Convert.ToDouble( rbzDT.Rows[ i ][ "c" ] );
                j++;
            }
            rbzDT.Clear();

            rbzDT = FillTable( "select * from Сейсм_режим_линеаментов where ind=" + IND + " ORDER BY n_magn ASC" );
            kj = 1;
            for(i = 0; i < rbzDT.Rows.Count; i++)
            {
                MGN[ kj ] = Convert.ToDouble( rbzDT.Rows[ i ][ "mgnt" ] );
                GR[ kj ] = Convert.ToDouble( rbzDT.Rows[ i ][ "grf" ] );
                kj++;
            }
            rbzDT.Clear();

            DIPF = DIPF / RAD;
            SDPF = Math.Sin( DIPF );
            if(SDPF < 1.0e-6)
            {
                wrng_out.WriteLine( "Разлом горизонтален, N=" + IND );
                wrng_out.WriteLine( "DIPF=" + DIPF );
                EmExit( "Ошибка в субмодуле INPFZ" );
                return -1;
            }
            outFile.WriteLine( "NUMB OF MAGS = " + INMAX );

            if(KUMUL == 1)
            {
                for(kj = 2; kj <= INMAX; kj++)
                {
                    if(GR[ kj ] > GR[ kj - 1 ])
                    {
                        wrng_out.WriteLine( "Зона " + IND );
                        wrng_out.WriteLine( "KUMUL=1, но график НЕКУМУЛЯТИВНЫЙ!!!" );
                        EmExit( "Ошибка в субмодуле INPFZ" );
                        return -1;
                    }
                }
            }
            for(kj = 1; kj <= INMAX; kj++)
            {
                if(KMAG == 0)
                {
                    CLCRB3( IMW,MGN[ kj ],OMLH,OMW,ref X );
                    MGN[ kj ] = X;
                }
            }
            if(KUMUL == 0)
            {
                GR[ INMAX + 1 ] = 0.0;
                for(kj = INMAX; kj >= 1; kj--)
                    GR[ kj ] = GR[ kj + 1 ] + GR[ kj ];
            }

            HA[ 1 ] = Convert.ToDouble( rlinDT.Rows[ li ][ "h1" ] );
            HA[ 2 ] = Convert.ToDouble( rlinDT.Rows[ li ][ "h2" ] );

            KDEP = Convert.ToInt64( rlinDT.Rows[ li ][ "kdep" ] );
            DPTHU = Convert.ToDouble( rlinDT.Rows[ li ][ "dpthu" ] );
            DPTHL = Convert.ToDouble( rlinDT.Rows[ li ][ "dpthl" ] );

            AKDIP = Convert.ToDouble( rlinDT.Rows[ li ][ "akdip" ] );
            AKAZ = Convert.ToDouble( rlinDT.Rows[ li ][ "akaz" ] );
            DEVL = Convert.ToDouble( rlinDT.Rows[ li ][ "devl" ] );
            DEVC = Convert.ToDouble( rlinDT.Rows[ li ][ "devc" ] );

            KSTIC = Convert.ToInt64( rlinDT.Rows[ li ][ "kstic" ] );
            THR1 = Convert.ToDouble( rlinDT.Rows[ li ][ "thr1" ] );
            PRT1 = Convert.ToDouble( rlinDT.Rows[ li ][ "p1" ] );
            PRT2 = Convert.ToDouble( rlinDT.Rows[ li ][ "p2" ] );

            if(KMAG == 0)
            {
                CLCRB3( IMW,THR1,OMLH,OMW,ref X );
                THR1 = X;
            }

            DPS1 = Convert.ToDouble( rlinDT.Rows[ li ][ "dips1" ] );
            DVS1 = Convert.ToDouble( rlinDT.Rows[ li ][ "sdip1" ] );
            DPS2 = Convert.ToDouble( rlinDT.Rows[ li ][ "dips2" ] );
            DVS2 = Convert.ToDouble( rlinDT.Rows[ li ][ "sdip2" ] );

            DPS1 = DPS1 / RAD;
            DVS1 = DVS1 / RAD;
            DPS2 = DPS2 / RAD;
            DVS2 = DVS2 / RAD;

            KPNTR = Convert.ToInt64( rlinDT.Rows[ li ][ "kpntr" ] );
            outFile.WriteLine( "KPNTR=" + KPNTR );

            if(KPNTR == 0)
            {
                PRT[ 1 ] = .5;
                PRT[ 2 ] = .5;
                PRT[ 3 ] = .5;
            }
            //	inp.GetNextLine(aa,300);
            X = PRT1 + PRT2;
            if(X < EPS)
            {
                EmExit( "Ошибка в субмодуле INPFZ Зона ВОЗ=" + IND + " [PRT1+PRT2=0]" );
                return -1;
            }// STOP'PRT1+PRT2=0'}

            PRT1 = PRT1 / X;
            PRT2 = PRT2 / X;
            for(j = 1; j <= NVS; j++)
            {
                X1 = XP[ j ] / RAD;
                Y1 = YP[ j ] / RAD;
                GEDECCON( PHI0,AL0,AZ0,X1,Y1,ref X,ref Y );
                XP[ j ] = X;
                YP[ j ] = Y;
            }
            RMIN = 1.0E+6;
            for(j = 1; j <= NETPNT; j++)
            {
                DSTPLG( XNET[ j ],YNET[ j ],XP,YP,NVS,ref DMIN );
                if(DMIN < RMIN)
                    RMIN = DMIN;
            }
            RMIN = Math.Sqrt( Math.Pow( RMIN,2.0 ) + Math.Pow( HA[ 1 ],2.0 ) );
            FLAZ( XP[ 1 ],YP[ 1 ],XP[ 2 ],YP[ 2 ],ref FLZL,ref FAZ );
            outFile.WriteLine( "Длина разлома = " + FLZL );
            K0 = 0;
            if(RMIN != 0.0)
            {
                for(j = 1; j <= INMAX; j++)
                {
                    CLCRB3( IM3,MGN[ j ],BSM3,DST3,ref R3M );
                    if(R3M < RMIN) { K0 = K0 + 1; }
                }
            }
            if(K0 == INMAX)
            {
                outFile.WriteLine( String.Format( "{0} {1} EXCLUDED",ZONE,IND ) );
                return ss;
            } else INMAX = INMAX - K0;

            for(j = 1; j <= INMAX; j++)
            {
                MGNT[ j ] = MGN[ j + K0 ];
                GRF[ j ] = GR[ j + K0 ] * SZON;
            }

            long ret;
            ret = PREPAREF( XP[ 1 ],YP[ 1 ],XP[ 2 ],YP[ 2 ],2 );
            if(ret == 2)
                return ss;

            if(INMA < INMAX)
            {
                for(i = 1; i <= INMA; i++) GRF[ i ] = GRF[ i ] - GRF[ INMA + 1 ];
                INMAX = INMA;
            }
            outFile.WriteLine( String.Format( "MINMAG, MAXMAG ==> {0},{1}",MGNT[ 1 ],MGNT[ INMAX ] ) );
            SLD = GRF[ 1 ];
            for(kj = 1; kj <= INMAX; kj++)
            {
                GR[ kj ] = GRF[ kj ] / SZON;
                GRF[ kj ] = GRF[ kj ] / SLD;
            }
            FLAZ( XP[ 1 ],YP[ 1 ],XP[ 2 ],YP[ 2 ],ref FLZL,ref FAZ );
            AZS = FAZ;
            FAZ = (FAZ + AZ0) * RAD;
            outFile.WriteLine( "AZIMUT OF THE FAULT = " + FAZ );
            return 0;
        }

        private void PROCLR(ref long I0)
        {
            long ret;
            if(ISBR == 1)
            {
                ret = INPLR( 2 );
                if(emexit == 1) return;
                if(ret == 2) goto a2;
            } else SOURLR( ref I0 );
            return;
a2:
            IGER = 1;
        }

        private long INPLR(long ss)
        {
            /*
            string aa = "";
            long KKR,KPOD;
            long k,Ilp,Jlp;
            double BR1,BR2;
            EPS = 1.0e-3;
            ZONE = "ZONA";
            
	String.Format("ZONA %ld",IND);
	outFile.WriteLine(str);
	grp.WriteLine(str);
	grp.WriteLine("MAG,GRFO,GRFM (KUMUL) ");
	
    ///ВЫБОР ИЗ СЛОЕВ?
    rsloiDT = FillTable( "select * from Слои ORDER BY ind ASC" );

	rsloi.GetFieldValue("nvs",oleaa);
	v=(LPVARIANT)oleaa;
	NVS=v->intVal;
	
	rsloi.GetFieldValue("kobh",oleaa);
	v=(LPVARIANT)oleaa;
	KOBH=v->intVal;

	
	rsloi.GetFieldValue("v2",oleaa);
	v=(LPVARIANT)oleaa;
	VH[1]=v->dblVal;
	
	rsloi.GetFieldValue("v6",oleaa);
	v=(LPVARIANT)oleaa;
	VH[2]=v->dblVal;
	
	rsloi.GetFieldValue("dp1",oleaa);
	v=(LPVARIANT)oleaa;
	DIPPLS[1]=v->dblVal;
	
	rsloi.GetFieldValue("dp2",oleaa);
	v=(LPVARIANT)oleaa;
	DIPPLS[2]=v->dblVal;
	
	rsloi.GetFieldValue("dp3",oleaa);
	v=(LPVARIANT)oleaa;
	DIPPLS[3]=v->dblVal;
	
	rsloi.GetFieldValue("dp4",oleaa);
	v=(LPVARIANT)oleaa;
	DIPPLS[4]=v->dblVal;

	String.Format("VH2,VH6,DP1,DP2,DP3,DP4= %.5f\t%.5f\t%.5f\t%.5f\t%.5f\t%.5f",VH[1],VH[2],DIPPLS[1],DIPPLS[2],DIPPLS[3],DIPPLS[4]);
	outFile.WriteLine(str);
	for( Jlp=1;Jlp<=4;Jlp++)
        DIPPLS[Jlp]=DIPPLS[Jlp]/RAD;

    String.Format("select * from Вершины_слоев where ind=%d ORDER BY n_verh ASC",IND);
	Jlp=1;
    
    rbz.Open(AFX_DAO_USE_DEFAULT_TYPE,str);
	if(rbz.GetRecordCount())rbz.MoveFirst();
	
	while(!rbz.IsEOF())
	{
		
		rbz.GetFieldValue("pf",oleaa);
		v=(LPVARIANT)oleaa;
		XPU[Jlp]=v->dblVal;
		
		rbz.GetFieldValue("pl",oleaa);
		v=(LPVARIANT)oleaa;
		YPU[Jlp]=v->dblVal;
		
		rbz.GetFieldValue("brm",oleaa);
		v=(LPVARIANT)oleaa;
		BRU[Jlp]=v->dblVal;
		Jlp++;
		rbz.MoveNext();
	}
	rbz.Close();
	for(Jlp=1;Jlp<=NVS;Jlp++)
	{
		if(KMAG==0)
		{
			CLCRB3(IMW,BRU[Jlp],OMLH,OMW,ref X);
			BRU[Jlp]=X;
		}
		XP[Jlp]=XPU[Jlp];
		YP[Jlp]=YPU[Jlp];
		BR[Jlp]=BRU[Jlp];
	}
	for(Jlp=1;Jlp<=NVS;Jlp++)
	{
		X1=XP[Jlp]/RAD;
		Y1=YP[Jlp]/RAD;
		GEDECCON(PHI0, AL0, AZ0,X1,Y1,X,Y);
		XP[Jlp]=X;
		YP[Jlp]=Y;
	}//	   NVS,XP,YP,VH(1),VH(2),DIPPLS,SNQ,QS,PRPD
	GETLRM(NVS,XP,YP,VH[1],VH[2]);
	
	rsloi.GetFieldValue("inmax",oleaa);
	v=(LPVARIANT)oleaa;
	INMAX=v->intVal;
	
	rsloi.GetFieldValue("kumul",oleaa);
	v=(LPVARIANT)oleaa;
	KUMUL=v->intVal;
	
	rsloi.GetFieldValue("szon",oleaa);
	v=(LPVARIANT)oleaa;
	SZON=v->dblVal;
	String.Format("NUMB OF MAGS %ld ",INMAX);
	outFile.WriteLine(str);
  	String.Format("select * from Сейсм_режим_слоев where ind=%d ORDER BY n_magn ASC",IND);
	rbz.Open(AFX_DAO_USE_DEFAULT_TYPE,str);
	if(rbz.GetRecordCount())rbz.MoveFirst();
	k=1;
	while(!rbz.IsEOF())
	{
		
		rbz.GetFieldValue("mgnt",oleaa);
		v=(LPVARIANT)oleaa;
		MGN[k]=v->dblVal;
		
		rbz.GetFieldValue("grf",oleaa);
		v=(LPVARIANT)oleaa;
		GR[k]=v->dblVal;
		k++;
		rbz.MoveNext();
	}
	rbz.Close();
    if(KUMUL==1)
	{
		for(k=2;k<=INMAX;k++)
		{
			if(GR[k]>GR[k-1])
			{
				CString str1;
				wrng_out.WriteLine("    ");
				String.Format("Зона %ld",IND);str1=str;
				wrng_out.WriteLine(str);
				String.Format("KUMUL=1, но график НЕКУМУЛЯТИВНЫЙ!!!");str1+=str;
				wrng_out.WriteLine(str);
				wrng_out.WriteLine("    ");
				String.Format("Ошибка в субмодуле INPLR"+str);
				EmExit(str);
				return -1;
			}
		}
	}
	for(k=1;k<=INMAX;k++)
	{
		if(KMAG==0)
		{
			CLCRB3(IMW,MGN[k],OMLH,OMW,X);
			MGN[k]=X;
		}
	}
    if(KUMUL==0)
	{
        GR[INMAX+1]=0.0;
        for(k=INMAX;k>=1;k--){GR[k]=GR[k+1]+GR[k];}
	}
	
	rsloi.GetFieldValue("h1",oleaa);
	v=(LPVARIANT)oleaa;
	HA[1]=v->dblVal;
	
	rsloi.GetFieldValue("h2",oleaa);
	v=(LPVARIANT)oleaa;
	HA[2]=v->dblVal;
	
	rsloi.GetFieldValue("kdep",oleaa);
	v=(LPVARIANT)oleaa;
	KDEP=v->intVal;
	
	rsloi.GetFieldValue("dpthu",oleaa);
	v=(LPVARIANT)oleaa;
	DPTHU=v->dblVal;
	
	rsloi.GetFieldValue("dpthl",oleaa);
	v=(LPVARIANT)oleaa;
	DPTHL=v->dblVal;

	
	rsloi.GetFieldValue("akdip",oleaa);
	v=(LPVARIANT)oleaa;
	AKDIP=v->dblVal;
	
	rsloi.GetFieldValue("akaz",oleaa);
	v=(LPVARIANT)oleaa;
	AKAZ=v->dblVal;

	
	rsloi.GetFieldValue("devl",oleaa);
	v=(LPVARIANT)oleaa;
	DEVL=v->dblVal;
	
	rsloi.GetFieldValue("devc",oleaa);
	v=(LPVARIANT)oleaa;
	DEVC=v->dblVal;
	
	rsloi.GetFieldValue("kstic",oleaa);
	v=(LPVARIANT)oleaa;
	KSTIC=v->intVal;

	
	rsloi.GetFieldValue("thr",oleaa);
	v=(LPVARIANT)oleaa;
	THR=v->dblVal;
	
	rsloi.GetFieldValue("azs",oleaa);
	v=(LPVARIANT)oleaa;
	AZS=v->dblVal;
	
	rsloi.GetFieldValue("saz",oleaa);
	v=(LPVARIANT)oleaa;
	SAZ=v->dblVal;

    if(KMAG==0)
	{
		CLCRB3(IMW,THR,OMLH,OMW,X);
        THR=X;
	}
	AZS=AZS/RAD-AZ0;
	SAZ=SAZ/RAD;
	
	rsloi.GetFieldValue("thr1",oleaa);
	v=(LPVARIANT)oleaa;
	THR1=v->dblVal;
	
	rsloi.GetFieldValue("p1",oleaa);
	v=(LPVARIANT)oleaa;
	PRT1=v->dblVal;
	
	rsloi.GetFieldValue("p2",oleaa);
	v=(LPVARIANT)oleaa;
	PRT2=v->dblVal;
    if(KMAG==0)
	{
        CLCRB3(IMW,THR1,OMLH,OMW,X);
        THR1=X;
	}
	
	rsloi.GetFieldValue("dips1",oleaa);
	v=(LPVARIANT)oleaa;
	DPS1=v->dblVal;
	
	rsloi.GetFieldValue("sdip1",oleaa);
	v=(LPVARIANT)oleaa;
	DVS1=v->dblVal;
	
	rsloi.GetFieldValue("dips2",oleaa);
	v=(LPVARIANT)oleaa;
	DPS2=v->dblVal;
	
	rsloi.GetFieldValue("sdip2",oleaa);
	v=(LPVARIANT)oleaa;
	DVS2=v->dblVal;

	String.Format("THR1,PRT1,PRT2 %.5f\t%.5f\t%.5f",THR1,PRT1,PRT2);
	outFile.WriteLine(str);
	String.Format("DPS1,DVS1,DPS2,DVS2 %.5f\t%.5f\t%.5f\t%.5f",DPS1,DVS1,DPS2,DVS2);
	outFile.WriteLine(str);
	X=PRT1+PRT2;
	if(X<EPS)
	{
		String.Format("Ошибка в субмодуле INPLR Зона ВОЗ=%d [PRT1+PRT2=0]",IND);
		EmExit(str);
		return -1;
	};// STOP 'PRT1+PRT2=0'
	PRT1=PRT1/X;
	PRT2=PRT2/X;
	DPS1=DPS1/RAD;
	DVS1=DVS1/RAD;
	DPS2=DPS2/RAD;
	DVS2=DVS2/RAD;

	
	rsloi.GetFieldValue("kpntr",oleaa);
	v=(LPVARIANT)oleaa;
	KPNTR=v->intVal;

	String.Format("KPNTR= %ld",KPNTR);
	outFile.WriteLine(str);

	rsloi.GetFieldValue("kkr",oleaa);
	v=(LPVARIANT)oleaa;
	KKR=v->intVal;
	
	rsloi.GetFieldValue("br1",oleaa);
	v=(LPVARIANT)oleaa;
	BR1=v->dblVal;
	
	rsloi.GetFieldValue("kpod",oleaa);
	v=(LPVARIANT)oleaa;
	KPOD=v->intVal;
	
	rsloi.GetFieldValue("br2",oleaa);
	v=(LPVARIANT)oleaa;
	BR2=v->dblVal;

	if(KMAG==0)
	{
		CLCRB3(IMW,BR1,OMLH,OMW,X);
		BR1=X;
		CLCRB3(IMW,BR2,OMLH,OMW,X);
		BR2=X;
	}
    if(KDEP==1)
	{
		for(Jlp=1;Jlp<=INMAX;Jlp++)
		{
			SMAG=MGN[Jlp];
			SQ0=Math.Pow((double)10,(SMAG-CMAG));
			if(SMAG<CMW1) ALBYW=CLW1;
			if(SMAG<CMW2&&SMAG>=CMW1)ALBYW=CLW1+(CLW2-CLW1)*(SMAG-CMW1)/(CMW2-CMW1);
			if(SMAG>=CMW2) ALBYW=CLW2;
			SL0=Math.Sqrt(SQ0*ALBYW);
			SW0=SQ0/SL0;
			if(SMAG>=THR1)
			{
					DIP=DPS1;
					RHS=SW0*Math.Sin(DIP);
					H0U1=DPTHU+(.5+AKDIP)*RHS;
					if(HA[1]>H0U1) H0U1=HA[1];
					H0L1=DPTHL-(.5-AKDIP)*RHS;
					if(HA[2]<H0L1) H0L1=HA[2];
					DIP=DPS2;
					RHS=SW0*Math.Sin(DIP);
					H0U2=DPTHU+(.5+AKDIP)*RHS;
					if(HA[1]>H0U2) H0U2=HA[1];
					H0L2=DPTHL-(.5-AKDIP)*RHS;
					if(HA[2]<H0L2) H0L2=HA[2];
					if(H0U1>H0L1&&H0U2>H0L2)
					{
						wrng_out.WriteLine("    ");
						String.Format("Зона %ld",IND);
						wrng_out.WriteLine(str);
						CString str1;str1=str;str1+="Условия на глубины противоречивы";
						wrng_out.WriteLine("    ");
						DIP1=DPS1*RAD;
						DIP2=DPS2*RAD;
						SDIP1=SDIP*RAD;
						wrng_out.WriteLine("    ");
						wrng_out.WriteLine("MAG     W      DPS1   DPS2    H1        H2    DPT   HU    DPTHL");
						String.Format("%.5f\t%.5f\t%.5f\t%.5f\t%.5f\t%.5f\t%.5f\t%.5f",SMAG,SW0,DIP1,DIP2,HA[1],HA[2],DPTHU,DPTHL);
						wrng_out.WriteLine(str);
						wrng_out.WriteLine("    ");
						String.Format("H0U1    H0L1   H0U2   H0L2 %.5f\t%.5f\t%.5f\t%.5f",H0U1,H0L1,H0U2,H0L2);
						wrng_out.WriteLine(str);
						wrng_out.WriteLine("    ");
						String.Format("Ошибка в субмодуле INPLR"+str);
						EmExit(str);
						return -1;
					}
			}
		}
	}
	RMIN=1.e+6;
	for(Jlp=1;Jlp<=NETPNT;Jlp++)
	{
		double zz=0.0;
		DSTPRPD(XNET[Jlp],YNET[Jlp],zz,ref DMIN);
		if(DMIN<RMIN){ RMIN=DMIN;}
	}
	String.Format("SZON=%.5f",SZON);
	outFile.WriteLine(str);
    K0=0;
	if(RMIN!=0.0)
	{
		for(Jlp=1;Jlp<=INMAX;Jlp++)
		{
			CLCRB3(IM3,MGN[Jlp],BSM3,DST3 ,R3M);
			if(R3M<RMIN ) {K0=K0+1;}
		}	
	}
	if(K0==INMAX)
	{
		String.Format("ZONE %ld EXCLUDED",IND);
		outFile.WriteLine(str);
		outFile.WriteLine(" ");
		return ss;
	}
	else
	{
		INMAX=INMAX-K0;
		for(Jlp=1;Jlp<=INMAX;Jlp++)
		{
			MGNT[Jlp]=MGN[Jlp+K0];
			GRF[Jlp]=GR[Jlp+K0]*SZON;
		}
	}
	String.Format("MINMAG,MAXMAG %.5f %.5f",MGNT[1],MGNT[INMAX]);
	outFile.WriteLine(str);
	SLD=GRF[1];
	for(k=1;k<=INMAX;k++)
	{
		GR[k]=GRF[k]/SZON;
		GRF[k]=GRF[k]/SLD;
	}
    if(KPNTR==1)
	{
		for(Ilp=1;Ilp<=6;Ilp++){ KPNT[Ilp]=KPNTR;}
		KPNT[1]=KKR;
		KPNT[3]=KPOD;
		PMG[1]= BR1;
		PMG[2]=BR[2];
		PMG[3]= BR2;
		PMG[4]=BR[4];
		PMG[5]=BR[1];
		PMG[6]=BR[3];
	}*/
            return 0;
        }

        private void DSTPRPD(double x,double y,double z,ref double d)
        {
            double[] C1 = new double[ 4 ],
                C2 = new double[ 4 ],
                C3 = new double[ 4 ],
                C4 = new double[ 4 ],
                C5 = new double[ 4 ],
                C6 = new double[ 4 ],
                C7 = new double[ 4 ],
                C8 = new double[ 4 ];
            double[] ff = new double[ 7 ];
            double D1 = 0f,D2 = 0f,D3 = 0f,D4 = 0f,D5 = 0f,D6 = 0f;
            for(long Iaa = 1; Iaa <= 3; Iaa++)
            {
                C1[ Iaa ] = PRPD[ 4,Iaa ];
                C2[ Iaa ] = C1[ Iaa ] + PRPD[ 1,Iaa ];
                C3[ Iaa ] = C1[ Iaa ] + PRPD[ 1,Iaa ] + PRPD[ 2,Iaa ];
                C4[ Iaa ] = C1[ Iaa ] + PRPD[ 2,Iaa ];
                C5[ Iaa ] = C1[ Iaa ] + PRPD[ 3,Iaa ];
                C6[ Iaa ] = C5[ Iaa ] + PRPD[ 1,Iaa ];
                C7[ Iaa ] = C5[ Iaa ] + PRPD[ 1,Iaa ] + PRPD[ 2,Iaa ];
                C8[ Iaa ] = C5[ Iaa ] + PRPD[ 2,Iaa ];
            }
            DSTPRG( x,y,z,C1,C2,C3,C4,ref D1 );
            DSTPRG( x,y,z,C2,C3,C7,C6,ref D2 );
            DSTPRG( x,y,z,C5,C6,C7,C8,ref D3 );
            DSTPRG( x,y,z,C1,C2,C8,C5,ref D4 );
            DSTPRG( x,y,z,C1,C2,C6,C5,ref D5 );
            DSTPRG( x,y,z,C3,C4,C8,C7,ref D6 );
            ff[ 0 ] = D1; ff[ 1 ] = D2; ff[ 2 ] = D3; ff[ 3 ] = D4; ff[ 4 ] = D5; ff[ 5 ] = D6;
            d = fmin( ref ff );
        }

        private void SOURLR(ref long I0)
        {
            double[] X0 = new double[ 4 ],XC = new double[ 4 ];
            long aaa = 50;
            long ret,aa = 12;
            long Klr;
            IGPM3 = 5;
            IORM3 = 5;
            IFLT = 0;
            MRND( INMAX,ref I0 );
            SMAG = MGNT[ I0 ];
            NORMRND( ref S1,ref S3 );
            SA0 = Math.Pow( (double)10,(SMAG - CMAG + S1 * SDEVA) );
            if(SMAG < CMW1) { ALBYW = CLW1; }
            if(SMAG < CMW2 && SMAG >= CMW1) { ALBYW = CLW1 + (CLW2 - CLW1) * (SMAG - CMW1) / (CMW2 - CMW1); }
            if(SMAG >= CMW2) { ALBYW = CLW2; }
            SL0 = Math.Sqrt( SA0 * ALBYW );
            SW0 = SL0 / ALBYW;
            IGIP = 0;
a10:
            IGIP = IGIP + 1;
            if(IGIP >= IGPM3 + 1) { goto a60; }
a11:
            S1 = RAND();
            S2 = RAND();
            S3 = RAND();
            for(Klr = 1; Klr <= 3; Klr++)
            {
                X0[ Klr ] = PRPD[ 4,Klr ] + S1 * PRPD[ 1,Klr ] + S2 * PRPD[ 2,Klr ] + S3 * PRPD[ 3,Klr ];
            }
            ret = CHECKIN( ref X0,aa );
            if(ret == 12) { goto a12; }
            goto a11;
a12:
            H0 = -X0[ 3 ];
            if(H0 < HA[ 1 ] || H0 > HA[ 2 ]) { goto a11; }
            ITEST = 0;
a50:
            ITEST = ITEST + 1;
            if(ITEST >= IORM3 + 1) { goto a10; }
            if(SMAG >= THR) { SRCAZ = AZS + (1.0 - 2.0 * RAND()) * SAZ; } else { SRCAZ = 2.0 * PI * RAND(); }
            if(SMAG < THR1)
            {
                CO = 1.0 - 2.0 * RAND();
                if(CO >= 1.0) DIP = 0.0;
                else if(CO <= -1.0) DIP = PI;
                else DIP = Math.Acos( CO );
            } else
            {
                P = RAND();
                P1 = RAND();
                if(P < PRT1) { DIP = DPS1 + (1 - 2.0 * P1) * DVS1; } else { DIP = DPS2 + (1 - 2.0 * P1) * DVS2; }
            }
            X0[ 3 ] = Math.Abs( X0[ 3 ] );
            SRCR( X0,ref US1,ref US2,ref US3,ref US4,ref XC );
            if(KPNTR == 1)
            {
                US1[ 3 ] = -US1[ 3 ];
                US2[ 3 ] = -US2[ 3 ];
                US3[ 3 ] = -US3[ 3 ];
                US4[ 3 ] = -US4[ 3 ];
                ret = TSTPLR( ref US1,SMAG,aaa,KDEP,DPTHU,DPTHL,KPNT,PMG ); if(ret == 50) { goto a50; }
                ret = TSTPLR( ref US2,SMAG,aaa,KDEP,DPTHU,DPTHL,KPNT,PMG ); if(ret == 50) { goto a50; }
                ret = TSTPLR( ref US3,SMAG,aaa,KDEP,DPTHU,DPTHL,KPNT,PMG ); if(ret == 50) { goto a50; }
                ret = TSTPLR( ref US4,SMAG,aaa,KDEP,DPTHU,DPTHL,KPNT,PMG ); if(ret == 50) { goto a50; }
                US1[ 3 ] = -US1[ 3 ];
                US2[ 3 ] = -US2[ 3 ];
                US3[ 3 ] = -US3[ 3 ];
                US4[ 3 ] = -US4[ 3 ];
            }
            SROT = Math.Sin( SRCAZ );
            CROT = Math.Cos( SRCAZ );
            SPAR[ 1 ] = SMAG;
            SPAR[ 2 ] = SL0;
            SPAR[ 3 ] = SW0;
            SPAR[ 4 ] = SRCAZ;
            SPAR[ 5 ] = DIP;
            SPAR[ 6 ] = US1[ 1 ];
            SPAR[ 7 ] = US1[ 2 ];
            SPAR[ 8 ] = US1[ 3 ];
            SPAR[ 9 ] = XC[ 1 ];
            SPAR[ 10 ] = XC[ 2 ];
            SPAR[ 11 ] = XC[ 3 ];
            RPAR[ 1 ] = SL0;
            RPAR[ 2 ] = SW0;
            RPAR[ 3 ] = XC[ 3 ];
            RPAR[ 4 ] = DIP - PI / 2.0;
            RPAR[ 5 ] = 0.0;
            RPAR[ 6 ] = 0.0;
            goto a59;
a60:
            if(KFAIL == 0)
            {
                try
                {
                    //fls.Open(NAME6,CFile::modeWrite|CFile::typeText|CFile::modeCreate);
                    fls = new StreamWriter( NAME6,false,Encoding.GetEncoding( 1251 ) );
                }
                catch(Exception ex)
                {
                    MessageBox.Show( "ERROR: " + ex.Message );
                    return;
                }

                //fls.WriteLine( "NOT SUITABLE: INDZ, MAG, L, W, AZ,DIP, PHI1, LMD1, H1" );
                KFAIL = 1;
            }
            DEGEDCON( PHI0,AL0,AZ0,US1[ 1 ],US1[ 2 ],ref PHI1,ref AL1 );
            PHI1 = PHI1 * RAD;
            AL1 = AL1 * RAD;
            AZBRK = (SRCAZ + AZ0) * RAD;
            DIPBRK = DIP * RAD;
            SH = US1[ 3 ];
            if(SH <= 0.0) { SH = -SH; }
            fls.WriteLine( String.Format( "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t",IND,SMAG,SL0,SW0,AZBRK,DIPBRK,PHI1,AL1,SH ) );
            IFLT = 1;
a59:
            return;
        }

        private long TSTPLR(ref double[] U,double SMAG,long ss,long KDEP,double DPTHU,double DPTHL,long[] KPNT,double[] PMG)
        {
            long aa = 10;
            long ret = CHECKIN( ref U,aa );
            if(ret == 10) goto a10;
            for(int Is = 1; Is <= 6; Is++)
            {
                if(ISD[ Is ] != 0 && KPNT[ Is ] != 0)
                    if(SMAG > PMG[ Is ]) return ss;
            }
a10:
            if(KDEP == 1)
                if(-U[ 3 ] > DPTHL || -U[ 3 ] < DPTHU) return ss;
            return 0;
        }

        private long CHECKIN(ref double[] P,long ss)
        {
            double[] U = new double[ 4 ];
            EPS = 1.0e-1;
            long I1 = 0;
            for(long II = 1; II <= 6; II++)
            {
                for(long JJ = 1; JJ <= 3; JJ++)
                {
                    SN[ JJ ] = SNQ[ II,JJ ];
                    if(II == 3)
                        Q[ JJ ] = QS[ 5,JJ ];
                    else if(II == 6)
                        Q[ JJ ] = QS[ 3,JJ ];
                    else
                        Q[ JJ ] = QS[ II,JJ ];
                    U[ JJ ] = P[ JJ ] - Q[ JJ ];
                }
                long aa = 3;
                SCPR( aa,SN,U,ref PR,ref UG );
                if(PR < (-EPS))
                {
                    ISD[ II ] = 1;
                    I1 = 1;
                } else
                    ISD[ II ] = 0;
            }

            if(I1 == 0)
                return ss;
            return 0;
        }

        private void SCPR(long N,double[] A,double[] B,ref double P,ref double U)
        {
            P = 0.0;
            double DA = 0.0;
            double DB = 0.0;
            long Isr;
            for(Isr = 1; Isr <= N; Isr++)
            {
                P = P + A[ Isr ] * B[ Isr ];
                DA = DA + A[ Isr ] * A[ Isr ];
                DB = DB + B[ Isr ] * B[ Isr ];
            }
            DA = Math.Sqrt( DA );
            DB = Math.Sqrt( DB );
            EPS = 1.0e-6;
            if(Math.Abs( DA ) < EPS || DB < EPS)
                U = -1.0;
            else
            {
                U = P / (DA * DB);
                if(U > 1.0) { U = 1.0; }
                if(U < -1.0) { U = -1.0; }
                U = Math.Acos( U );
            }
        }

        private void GSTPRC(System.ComponentModel.BackgroundWorker worker, long NETPNT,double PHI0,double AL0,double AZ0,double NCYCL,double[] XNET,double[] YNET,double GI0,double TMAX, ResponseSpectra[] RS)
        {
            double gs1 = 0.0,gs2 = 0.0;

            for (int i_rs=1; i_rs<= NETPNT; i_rs++)
            {
                RS[i_rs].SAItogCalculation();

              //  RS[i_rs].ProbabilityCalculation(5);
              //RS[i_rs].SAsave(Convert.ToString(i_rs));
            }

            double[] IGS = new double[ IMGS + 1 ];
            long J,k,K1;
            double GJ;
            double[] CAM = new double[ IMGS + 1 ], //101 --> 31
                CAMN = new double[ IMGS + 1 ],
                AM = new double[ IMGS + 1 ],
                DEV1 = new double[ IMGS + 1 ],
                DEV2 = new double[ IMGS + 1 ],
                V1 = new double[ IMGS + 1 ],
                V2 = new double[ IMGS + 1 ],
                X = new double[ 4 ];

            //gst.WriteLine( "GI0,\tDI,\tTMAX,\tNCYCL" );
            //gst.WriteLine( String.Format( "{0}\t{1}\t{2}\t{3}",GI0,DI,TMAX,NCYCL ) );

            i_gst3 = 0;

            double[ , ] massiv_ABCD = new double[ 15,NETPNT+1 ];
            if (KPIECE == 1)
            {
                fla.Write("lat\tlon\tT{0}\tT{1}\tT{2}\tT{3}\tT{4}\tT{5}\tT{6}\tVI\tVII\tVIII\tIX\t",
                periodsOfRepeating[0],
                periodsOfRepeating[1],
                periodsOfRepeating[2],
                periodsOfRepeating[3],
                periodsOfRepeating[4],
                periodsOfRepeating[5],
                periodsOfRepeating[6]);



                for (int i = 0; i < 7; i++)
                {
                    fla.Write("PGA_{0}\tSA_0_1_{0}\tSA_0_2_{0}\tSA_0_3_{0}\tSA_0_4_{0}\tSA_0_5_{0}\tSA_0_7_{0}\tSA_1_{0}\tSA_2_{0}\tSA_3_{0}\tSA_5_{0}\t",
        periodsOfRepeating[i]);
                }
                fla.Write("d_{0}\td_{1}\td_{2}\td_{3}\td_{4}\td_{5}\td_{6}\t",
        periodsOfRepeating[0],
        periodsOfRepeating[1],
        periodsOfRepeating[2],
        periodsOfRepeating[3],
        periodsOfRepeating[4],
        periodsOfRepeating[5],
        periodsOfRepeating[6]);

                fla.Write("\n");
            }
            
            gst.WriteLine("LAT\tLON\tBALL\tIGS\tKUM\tKUMNORM\tGRAN1\tGRAN2");//////////////////////////////////////////////////////////////////////////////////////////////////////
            i_gst2 = 0;
            for (k = 1; k <= NETPNT; k++)
            {

                DEGEDCON( PHI0,AL0,AZ0,XNET[ k ],YNET[ k ],ref X[ 1 ],ref X[ 2 ] );
                X[ 1 ] = X[ 1 ] * RAD;
                X[ 2 ] = X[ 2 ] * RAD;

                gs1 = X[ 1 ];
                gs2 = X[ 2 ];

                mass_gist_lat_lon[ i_gst2,0 ] = X[ 1 ];
                mass_gist_lat_lon[ i_gst2,1 ] = X[ 2 ];

                i_gst2++;


                for(K1 = 1; K1 <= IMGS; K1++)
                {
                    IGS[ K1 ] = IGST[ k,K1 ];
                    AM[ K1 ] = IGST[ k,K1 ];
                }
a115:
                CAMN[ IMGS ] = 0.0;
                for(J = IMGS - 1; J >= 1; J--)
                    CAM[ J ] = CAM[ J + 1 ] + AM[ J + 1 ];
a1:
                for(J = 1; J <= IMGS; J++)
                {
                    V1[ J ] = CAM[ J ] - Math.Sqrt( CAM[ J ] );
                    V2[ J ] = CAM[ J ] + Math.Sqrt( CAM[ J ] );
                }
a2:
                PNUM = NCYCL;
                RSKVN( PNUM,CAM,CAMN,ref X[ 3 ] );
                RSKVN( PNUM,V1,DEV1,ref Y1 );
                RSKVN( PNUM,V2,DEV2,ref Y2 );
                JMAX = 1;

                for(J = 2; J <= IMGS; J++)
                {
                    if(CAM[ J - 1 ] < 1.0)
                        goto a21;
                    JMAX = JMAX + 1;
                }
a21:
                for(J = 2; J <= JMAX; J++)
                {
                    GJ = GI0 + (J - 2) * DI;
                    //String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t", GJ, IGS[J], CAM[J - 1], CAMN[J - 1], DEV1[J - 1], DEV2[J - 1]);
                    //gst.WriteLine( str );///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if(m_main_base_ok)
                    {
                        
                        str = String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}",
                            gs1, gs2, GJ, IGS[J], CAM[J - 1], CAMN[J - 1], DEV1[J - 1], DEV2[J - 1]);
                        InsertCommand( str );///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                    if (GJ==6) 
                    {
                        POVTOR[0, k] = gs1;
                        POVTOR[1, k] = gs2;
                        POVTOR[2, k] = TPR / CAMN[J - 1];
                    }
                    if (GJ == 7)
                    {
                        POVTOR[3, k] = TPR / CAMN[J - 1];
                    }
                    if (GJ == 8)
                    {
                        POVTOR[4, k] = TPR / CAMN[J - 1];
                    }
                    if (GJ == 9)
                    {
                        POVTOR[5, k] = TPR / CAMN[J - 1];
                    }
                    gst.WriteLine(str);
                }
                i_gst3++;

a12:
                massiv_ABCD[ 0,k ] = X[ 1 ];
                massiv_ABCD[ 1,k ] = X[ 2 ];

                PNUM1 = PNUM * (5000.0 / periodsOfRepeating[4]); //5000
                RSKVN(PNUM1, CAM, CAMN, ref X[3]);

                massiv_ABCD[ 8,k ] = X[ 3 ];
                massiv_ABCD[ 9,k ] = Y1;
                massiv_ABCD[ 10,k ] = Y2;

                if(m_main_base_ok)
                {
                    //str = String.Format( "INSERT INTO C (ind, lat, lon, ball, ball_minus, ball_plass) VALUES ({0},{1},{2},{3},{4},{5})",i_az,X[ 1 ],X[ 2 ],X[ 3 ],Y1,Y2 );
                    InsertCommand( str );
                    i_az++;
                }

                //a101:	  FORMAT(1X,5(2X,F8.4))//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                PNUM1 = PNUM * (5000.0 / periodsOfRepeating[1]); //2 значение
                RSKVN( PNUM1,CAM,CAMN,ref X[ 3 ] );
                RSKVN( PNUM1,V1,DEV1,ref Y1 );
                RSKVN( PNUM1,V2,DEV2,ref Y2 );

                massiv_ABCD[ 2,k ] = X[ 3 ];
                massiv_ABCD[ 3,k ] = Y1;
                massiv_ABCD[ 4,k ] = Y2;

                if(m_main_base_ok)
                {
                    //str = String.Format( "INSERT INTO A (ind, lat, lon, ball,ball_minus,ball_plass) VALUES ({0},{1},{2},{3},{4},{5})",i_bz,X[ 1 ],X[ 2 ],X[ 3 ],Y1,Y2 );
                    InsertCommand( str );
                    //	i_bz++;
                }
                //a101:	  FORMAT(1X,5(2X,F8.4))//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                PNUM1 = PNUM * (5000.0 / periodsOfRepeating[2]); //3 значение
                RSKVN( PNUM1,CAM,CAMN,ref X[ 3 ] );
                RSKVN( PNUM1,V1,DEV1,ref Y1 );
                RSKVN( PNUM1,V2,DEV2,ref Y2 );

                massiv_ABCD[ 5,k ] = X[ 3 ];
                massiv_ABCD[ 6,k ] = Y1;
                massiv_ABCD[ 7,k ] = Y2;

                if(m_main_base_ok)
                {
                    //str = String.Format( "INSERT INTO B (ind, lat, lon, ball,ball_minus,ball_plass) VALUES ({0},{1},{2},{3},{4},{5})",i_bz,X[ 1 ],X[ 2 ],X[ 3 ],Y1,Y2);
                    InsertCommand( str );
                    i_bz++;
                }

                PNUM1 = PNUM * (5000.0 / periodsOfRepeating[5]);//10000 лет
                RSKVN( PNUM1,CAM,CAMN,ref X[ 3 ] );


                PNUM1 = PNUM * (5000.0 / periodsOfRepeating[3]);//2500 лет
                RSKVN(PNUM1, CAM, CAMN, ref Y1);

                PNUM1 = PNUM * (5000.0 / periodsOfRepeating[0]);//1 значение
                RSKVN(PNUM1, CAM, CAMN, ref Y2);
                
                massiv_ABCD[ 11,k ] = X[ 3 ];
                massiv_ABCD[ 12,k ] = Y1;
                massiv_ABCD[ 13,k ] = Y2;

                PNUM1 = PNUM * (5000.0 / periodsOfRepeating[6]);//100000 лет
                RSKVN(PNUM1, CAM, CAMN, ref Y3);
                massiv_ABCD[14, k] = Y3;

                if(m_main_base_ok)
                {
                    //str = String.Format("INSERT INTO D (ind, lat, lon, ball,ball_minus,ball_plass) VALUES ({0},{1},{2},{3},{4},{5})", i_cz, X[1], X[2], X[3], Y1, Y2);
                    InsertCommand( str );
                    i_cz++;
                }

                double[] TP = CalculateDuration(k, massiv_ABCD);

                NameOfCurrentCalculation = "Сохранение подсетки № " + Convert.ToString(KPIECE) + " ( " + Convert.ToString(k) + " из " + Convert.ToString(NETPNT) + " )";
                worker.ReportProgress(percents, NameOfCurrentCalculation);

                //ОКОНЧАТЕЛЬНАЯ запись в файл всех параметров:\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                fla.Write(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t",
                    massiv_ABCD[0, k],
                    massiv_ABCD[1, k],
                    massiv_ABCD[13, k],
                    massiv_ABCD[2, k],
                    massiv_ABCD[5, k],
                    massiv_ABCD[12, k],
                    massiv_ABCD[8, k],
                    massiv_ABCD[11, k],
                    massiv_ABCD[14, k],
                    POVTOR[2, k],
                    POVTOR[3, k],
                    POVTOR[4, k],
                    POVTOR[5, k]));



                for (int i = 0; i < 7; i++)
                {
                    fla.Write(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t",
                        RS[k].PGASACalculation(0.01, probabilityOfExceedance[i]),
                        RS[k].PGASACalculation(0.1, probabilityOfExceedance[i]),
                        RS[k].PGASACalculation(0.2, probabilityOfExceedance[i]),
                        RS[k].PGASACalculation(0.3, probabilityOfExceedance[i]),
                        RS[k].PGASACalculation(0.4, probabilityOfExceedance[i]),
                        RS[k].PGASACalculation(0.5, probabilityOfExceedance[i]),
                        RS[k].PGASACalculation(0.7, probabilityOfExceedance[i]),
                        RS[k].PGASACalculation(1, probabilityOfExceedance[i]),
                        RS[k].PGASACalculation(2, probabilityOfExceedance[i]),
                        RS[k].PGASACalculation(3, probabilityOfExceedance[i]),
                        RS[k].PGASACalculation(5, probabilityOfExceedance[i])));
                }

                fla.Write(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t",
    TP[0],
    TP[1],
    TP[2],
    TP[3],
    TP[4],
    TP[5],
    TP[6]));

                fla.Write("\n");
               
               // POVTOR_BALL.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", POVTOR[0,k], POVTOR[1,k], POVTOR[2,k], POVTOR[3,k], POVTOR[4,k], POVTOR[5,k]));


            }

            //TODO : Проверить. При закрытии файла - деагрегация не выполняется. Скорее всего связано с подсетками.
            fla.Close();

            if (DEAG == 2)
            {
                if (File.Exists(NAMEA))
                {
                    File.Copy(NAMEA, Path.GetDirectoryName(NAMEA) + "\\" + Path.GetFileNameWithoutExtension(NAMEA) + "_.TXT");
                }
            }

            //сохранение массивов с длительностями
           // if (calculateDuraion && DEAG != 1)
          //      SaveDurationMassive();


            flag_9999 = 1;

            if (DEAG == 1)//Запись в файлы результаты деагрегации
            {
                /* int jjj = 0;
                 while (jjj < NETPNT)
                 {

                     //Запись в файл деагрегации по баллам М-R
                     double[] SUMDEAGREG = new double[10];

                     NAME5 = "deagreg_"+ (jjj+1) + ".TXT";

                     //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                     DEAGREGA = new StreamWriter(NAME5, false, Encoding.GetEncoding(1251));
                     DEAGREGA.WriteLine("Mlh\tR\tT{0}\tT{1}\tT{2}\tT{3}\tT{4}\tT{5}\tT{6}",
                         periodsOfRepeating[0],
                         periodsOfRepeating[1],
                         periodsOfRepeating[2],
                         periodsOfRepeating[3],
                         periodsOfRepeating[4],
                         periodsOfRepeating[5],
                         periodsOfRepeating[6]
                         );


                     for (int i = 1; i <= 9; i++)
                     {
                         SUMDEAGREG[i] = 0 ;//считаем сумму по столбцам
                     }

                     for (int i = 3; i <= 9; i++)
                     {
                         for (jdeg = 1; jdeg <= 770; jdeg++)
                         {
                             SUMDEAGREG[i] = DEAGREG[i, jdeg + (770 * jjj)] + SUMDEAGREG[i];//считаем сумму по столбцам

                         }
                     }
                     for (jdeg = 1; jdeg <= 770; jdeg++)//запись в файл
                     {
                         //вывод в процентах
                         DEAGREGA.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", DEAGREG[1, jdeg + (770 * jjj)], DEAGREG[2, jdeg + (770 * jjj)], (DEAGREG[3, jdeg + (770 * jjj)] / SUMDEAGREG[3]) * 100, DEAGREG[4, jdeg + (770 * jjj)] / SUMDEAGREG[4] * 100, DEAGREG[5, jdeg + (770 * jjj)] / SUMDEAGREG[5] * 100, DEAGREG[6, jdeg + (770 * jjj)] / SUMDEAGREG[6] * 100, DEAGREG[7, jdeg + (770 * jjj)] / SUMDEAGREG[7] * 100, DEAGREG[8, jdeg + (770 * jjj)] / SUMDEAGREG[8]*100, DEAGREG[9, jdeg + (770 * jjj)] / SUMDEAGREG[9]*100));
                      // DEAGREGA.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", DEAGREG[1, jdeg + (770 * jjj)], DEAGREG[2, jdeg + (770 * jjj)], DEAGREG[3, jdeg + (770 * jjj)], DEAGREG[4, jdeg + (770 * jjj)], DEAGREG[5, jdeg + (770 * jjj)], DEAGREG[6, jdeg + (770 * jjj)], DEAGREG[7, jdeg + (770 * jjj)], DEAGREG[8, jdeg + (770 * jjj)], DEAGREG[9, jdeg + (770 * jjj)]));

                     }
                     DEAGREGA.Close();
                     jjj++;
                 }



                //Запись деагрегации для спектров реакций M-R
                int jjj = 0;
                while (jjj < NETPNT)
                {
                    double[] SUMDEAGREG_RS = new double[10];

                    NAMEDEAG_RS = "deagregResponseSpectra_" + (jjj + 1) + ".TXT";

                    DEAGREGA_RS = new StreamWriter(NAMEDEAG_RS, false, Encoding.GetEncoding(1251));
                    DEAGREGA_RS.WriteLine("Mlh\tR\tPGA_T{0}\tPGA_T{1}\tPGA_T{2}\tPGA_T{3}\tPGA_T{4}\tPGA_T{5}\tPGA_T{6}",
                        periodsOfRepeating[0],
                        periodsOfRepeating[1],
                        periodsOfRepeating[2],
                        periodsOfRepeating[3],
                        periodsOfRepeating[4],
                        periodsOfRepeating[5],
                        periodsOfRepeating[6]
                        );

                    for (int i = 1; i <= 9; i++)
                    {
                        SUMDEAGREG_RS[i] = 0;//считаем сумму по столбцам
                    }
                    
                    for (int i = 3; i <= 9; i++)
                    {
                        for (jdeg = 1; jdeg <= 770; jdeg++)
                        {
                            SUMDEAGREG_RS[i] = DEAGREGRespSpectr[i, jdeg + (770 * jjj)] + SUMDEAGREG_RS[i];//считаем сумму по столбцам

                        }
                    }

                    for (jdeg = 1; jdeg <= 770; jdeg++)//запись в файл
                    {
                        //вывод в процентах
                        DEAGREGA_RS.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",
                            DEAGREGRespSpectr[1, jdeg + (770 * jjj)],
                            DEAGREGRespSpectr[2, jdeg + (770 * jjj)],
                            (DEAGREGRespSpectr[3, jdeg + (770 * jjj)] / SUMDEAGREG_RS[3]) * 100,
                            DEAGREGRespSpectr[4, jdeg + (770 * jjj)] / SUMDEAGREG_RS[4] * 100,
                            DEAGREGRespSpectr[5, jdeg + (770 * jjj)] / SUMDEAGREG_RS[5] * 100,
                            DEAGREGRespSpectr[6, jdeg + (770 * jjj)] / SUMDEAGREG_RS[6] * 100,
                            DEAGREGRespSpectr[7, jdeg + (770 * jjj)] / SUMDEAGREG_RS[7] * 100,
                            DEAGREGRespSpectr[8, jdeg + (770 * jjj)] / SUMDEAGREG_RS[8] * 100,
                            DEAGREGRespSpectr[9, jdeg + (770 * jjj)] / SUMDEAGREG_RS[9] * 100));
                       

                    }
                    DEAGREGA_RS.Close();
                    jjj++;
                }*/





                   int jjj = 0;//запись деагрегации для доменов и линеаментов
                while (jjj < NETPNT)
                {
                    double[] SUMDEAGREGDOMLIN = new double[10];

                    NAMEDEAGDOMLIN = "deagregDomLin_" + (jjj + 1) + ".TXT";

                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    DEAGREGA2 = new StreamWriter(NAMEDEAGDOMLIN, false, Encoding.GetEncoding(1251));
                    DEAGREGA2.WriteLine("N\tT{0}\tT{1}\tT{2}\tT{3}\tT{4}\tT{5}\tT{6}",
                        periodsOfRepeating[0],
                        periodsOfRepeating[1],
                        periodsOfRepeating[2],
                        periodsOfRepeating[3],
                        periodsOfRepeating[4],
                        periodsOfRepeating[5],
                        periodsOfRepeating[6]
                        );


                    for (int i = 1; i < 8; i++)
                    {
                        for (jdeg = 0; jdeg < total; jdeg++)
                        {
                            SUMDEAGREGDOMLIN[i] = DeargLineamDoman[jdeg, i] + SUMDEAGREGDOMLIN[i];//считаем сумму по столбцам

                        }
                    }

                    for (jdeg = 0; jdeg < total; jdeg++)//запись в файл
                    {
                        /*вывод в процентах*/
                        DEAGREGA2.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", 
                            DeargLineamDoman[jdeg, 0],
                            DeargLineamDoman[jdeg, 1]/ SUMDEAGREGDOMLIN[1],
                            DeargLineamDoman[jdeg, 2]/ SUMDEAGREGDOMLIN[2],
                            DeargLineamDoman[jdeg, 3]/ SUMDEAGREGDOMLIN[3],
                            DeargLineamDoman[jdeg, 4]/ SUMDEAGREGDOMLIN[4],
                            DeargLineamDoman[jdeg, 5]/ SUMDEAGREGDOMLIN[5],
                            DeargLineamDoman[jdeg, 6]/ SUMDEAGREGDOMLIN[6],
                            DeargLineamDoman[jdeg, 7]/ SUMDEAGREGDOMLIN[7] ));
                        // DEAGREGA.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}", DEAGREG[1, jdeg + (770 * jjj)], DEAGREG[2, jdeg + (770 * jjj)], DEAGREG[3, jdeg + (770 * jjj)], DEAGREG[4, jdeg + (770 * jjj)], DEAGREG[5, jdeg + (770 * jjj)], DEAGREG[6, jdeg + (770 * jjj)], DEAGREG[7, jdeg + (770 * jjj)], DEAGREG[8, jdeg + (770 * jjj)], DEAGREG[9, jdeg + (770 * jjj)]));

                    }
                    DEAGREGA2.Close();
                    jjj++;
                }

                //Сохранение деагрегации
                currentDeag.SaveDeagreg(periodsOfRepeating);
                
                DeargLineamDoman[iiIND, 2]++;

            }            


        }

        //Вычисление длительности
        private double[] CalculateDuration(long k, double[,] massiv_ABCD)
        {
            double[] TP = new double[7];


            //DurrationMassive[num, i, j]
            double X1, X2, Y1, Y2;
            


            NIo[0] = Convert.ToInt32(Math.Round(massiv_ABCD[13, k], 1, MidpointRounding.AwayFromZero) * 10 - 54);
            if (NIo[0] < 1)
                NIo[0] = 1;
            else if (NIo[0] > 61)
                NIo[0] = 61;

            NIo[1] = Convert.ToInt32(Math.Round(massiv_ABCD[2, k], 1, MidpointRounding.AwayFromZero) * 10 - 54);
            if (NIo[1] < 1)
                NIo[1] = 1;
            else if (NIo[1] > 61)
                NIo[1] = 61;

            NIo[2] = Convert.ToInt32(Math.Round(massiv_ABCD[5, k], 1, MidpointRounding.AwayFromZero) * 10 - 54);
            if (NIo[2] < 1)
                NIo[2] = 1;
            else if (NIo[2] > 61)
                NIo[2] = 61;

            NIo[3] = Convert.ToInt32(Math.Round(massiv_ABCD[12, k], 1, MidpointRounding.AwayFromZero) * 10 - 54);
            if (NIo[3] < 1)
                NIo[3] = 1;
            else if (NIo[3] > 61)
                NIo[3] = 61;

            NIo[4] = Convert.ToInt32(Math.Round(massiv_ABCD[8, k], 1, MidpointRounding.AwayFromZero) * 10 - 54);
            if (NIo[4] < 1)
                NIo[4] = 1;
            else if (NIo[4] > 61)
                NIo[4] = 61;

            NIo[5] = Convert.ToInt32(Math.Round(massiv_ABCD[11, k], 1, MidpointRounding.AwayFromZero) * 10 - 54);
            if (NIo[5] < 1)
                NIo[5] = 1;
            else if (NIo[5] > 61)
                NIo[5] = 61;

            NIo[6] = Convert.ToInt32(Math.Round(massiv_ABCD[14, k], 1, MidpointRounding.AwayFromZero) * 10 - 54);
            if (NIo[6] < 1)
                NIo[6] = 1;
            else if (NIo[6] > 61)
                NIo[6] = 61;

            for (int i = 0; i < 62; i++)
            {
                A2[i, 0] = DurrationMassive[k-1, i, 0];
                A3[i, 0] = DurrationMassive[k-1, i, 0];
            }

            for (int i = 0; i < 62; i++)
            {
                A2[0, i] = DurrationMassive[k-1, 0, i];
                A3[0, i] = DurrationMassive[k-1, 0, i];
            }

            for (int i = 1; i < 62; i++)
            {
               for (int j = 1; j < 62; j++)
                {
                    double A = 0;
                    for (int jj = j; jj < 62; jj++)
                    {
                        A += DurrationMassive[k-1, i, jj];
                    }
                     A2[i, j] = A;
                }
            }

            for (int i = 1; i < 62; i++)
            {
                for (int j = 1; j < 62; j++)
                {
                    double A_up = 0;
                    double A_down = 0;

                    for (int ii = 1; ii <= i; ii++)
                    {
                        A_up += A2[ii,j];
                    }

                    for (int ii = 1; ii <= 61; ii++)
                    {
                        A_down += A2[ii, j];
                    }

                    A3[i, j] = Double.IsNaN((A_up * 100) / A_down) ? 0 : (A_up * 100) / A_down;
                }
            }

            for (int i = 0; i < 62; i++)
            {
                A4[i, 0] = DurrationMassive[k - 1, i, 0];
            }

            for (int nn = 1; nn < 8; nn++)
            {
                A4[0, nn] = DurrationMassive[k - 1, 0, NIo[nn - 1]];
            }

            for (int i = 1; i < 62; i++)
            {
                for (int nn = 1; nn < 8; nn++)
                {
                    A4[i, nn] = A3[i, NIo[nn - 1]];
                }
            }


            for (int i = 1; i <= 7; i++)
            {
                X1 = 0;
                X2 = 0;
                Y1 = 0;
                Y2 = 0;
                for (int j = 1; j <= 61; j++)
                {
                    if (((A4[j - 1, i] <= (100 - probabilityOfExceedance[i - 1])) && (A4[j, i] >= (100 - probabilityOfExceedance[i - 1]))) || ((A4[j - 1, i] >= (100 - probabilityOfExceedance[i - 1])) && (A4[j, i] <= (100 - probabilityOfExceedance[i - 1]))))
                    {

                        X1 = A4[j - 1, i];
                        X2 = A4[j, i];
                        Y1 = A4[j - 1, 0];
                        Y2 = A4[j, 0];

                        if(X1 != X2)
                        {
                            TP[i - 1] = ((Y2 - Y1) / (X2 - X1)) * (100 - probabilityOfExceedance[i - 1]) + Y1 - ((Y2 - Y1) / (X2 - X1)) * X1;
                        }
                        
                    }

                }
            }
            /*

            String NameDIR;
            NameDIR = Application.StartupPath;
            StreamWriter Filewriter = new StreamWriter(NameDIR + "\\A3.txt");
            for (int i = 0; i <= 61; i++)
            {
                for (int j = 0; j <= 61; j++)
                {
                    Filewriter.Write("{0}\t", A3[i, j]);
                }
                Filewriter.Write("\n");
            }

            StreamWriter Filewriter2 = new StreamWriter(NameDIR + "\\A4.txt");
            for (int i = 0; i <= 61; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    Filewriter2.Write("{0}\t", A4[i, j]);
                }
                Filewriter2.Write("\n");
            }

            for (int j = 0; j < 7; j++)
            {
                Filewriter2.Write("{0}\t", TP[j]);
            }
            
            Filewriter2.Close();
            Filewriter.Close();

            */

            return TP;

        }

        private void VECPR(long N,double[] A,double[] B,ref double[] C,ref double CL)
        {
            if(N == 3)
            {
                C[ 1 ] = A[ 2 ] * B[ 3 ] - A[ 3 ] * B[ 2 ];
                C[ 2 ] = -(A[ 1 ] * B[ 3 ] - A[ 3 ] * B[ 1 ]);
                C[ 3 ] = A[ 1 ] * B[ 2 ] - A[ 2 ] * B[ 1 ];
                CL = 0.0;
                for(long Id = 1; Id <= N; Id++)
                {
                    CL = CL + Math.Pow( C[ Id ],2.0 );
                }

                CL = Math.Sqrt( CL );
            } else if(N == 2)
            {
                C[ 1 ] = A[ 1 ] * B[ 2 ] - A[ 2 ] * B[ 1 ];
                CL = Math.Abs( C[ 1 ] );
            }
        }

        private void PLN(long N2,long N3,double[] DIP,long N1)
        {
            double[] E = new double[ 4 ],E1 = new double[ 4 ],E2 = new double[ 4 ];
            PI = 4.0 * Math.Atan( 1.0 );
            double Sl = 0.0;
            for(int Ipln = 1; Ipln <= 2; Ipln++)
            {
                E[ Ipln ] = QS[ N3,Ipln ] - QS[ N2,Ipln ];
                Sl = Sl + Math.Pow( E[ Ipln ],2.0 );
            }
            Sl = Math.Sqrt( Sl );
            for(int Ipln = 1; Ipln <= 2; Ipln++) { E[ Ipln ] = E[ Ipln ] / Sl; }
            double SINAZ = E[ 1 ];
            double COSAZ = E[ 2 ];
            double COSD = Math.Cos( DIP[ N1 ] );
            E1[ 1 ] = COSAZ * COSD;
            E1[ 2 ] = -SINAZ * COSD;
            E1[ 3 ] = -Math.Sin( DIP[ N1 ] );
            E2[ 1 ] = SINAZ;
            E2[ 2 ] = COSAZ;
            E2[ 3 ] = 0.0;
            VECPR( 3,E1,E2,ref E,ref CL );
            for(int Ipln = 1; Ipln <= 3; Ipln++)
                SNQ[ N1,Ipln ] = E[ Ipln ];
        }

        private void GETLRM(long NVS,double[] PX,double[] PY,double H1,double H2)
        {
            double[] E1 = new double[ 4 ],E2 = new double[ 4 ],F1 = new double[ 4 ],F2 = new double[ 4 ],PS = new double[ 4 ];
            double[ , ] E = new double[ 4,4 ];
            long Ilrm,Jlrm;
            PI = 4.0 * Math.Atan( 1.0 );
            for(Ilrm = 1; Ilrm <= NVS; Ilrm++)
            {
                QS[ Ilrm,1 ] = PX[ Ilrm ];
                QS[ Ilrm,2 ] = PY[ Ilrm ];
            }
            PLN( 2,3,DIPPLS,1 );
            if(DIPPLS[ 1 ] < PI / 2.0)
            {
                for(Ilrm = 1; Ilrm <= 3; Ilrm++) { SNQ[ 1,Ilrm ] = -SNQ[ 1,Ilrm ]; }
            }
            PLN( 2,3,DIPPLS,2 );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { SNQ[ 2,Ilrm ] = -SNQ[ 2,Ilrm ]; }
            QS[ 2,3 ] = -H1;
            QS[ 3,3 ] = -H1;
            PLN( 2,3,DIPPLS,3 );
            if(DIPPLS[ 3 ] > PI / 2.0)
            {
                for(Ilrm = 1; Ilrm <= 3; Ilrm++) { SNQ[ 3,Ilrm ] = -SNQ[ 3,Ilrm ]; }
            }
            DIPPLS[ 5 ] = PI / 2.0;
            PLN( 1,2,DIPPLS,5 );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { SNQ[ 5,Ilrm ] = -SNQ[ 5,Ilrm ]; }
            DIPPLS[ 6 ] = PI / 2.0;
            PLN( 3,4,DIPPLS,6 );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { SNQ[ 6,Ilrm ] = -SNQ[ 6,Ilrm ]; }
            DIPPLS[ 7 ] = PI / 2.0;
            PLN( 1,4,DIPPLS,7 );
            PLN( 1,4,DIPPLS,7 );
            QS[ 1,3 ] = 0.0;
            SECPLNS( 1,2,2,7,1,5,ref PS );
            QS[ 1,3 ] = PS[ 3 ];
            SECPLNS( 1,2,3,7,1,6,ref PS );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { QS[ 4,Ilrm ] = PS[ Ilrm ]; }
            double Sl = 0.0;
            for(Ilrm = 1; Ilrm <= 3; Ilrm++)
            {
                E2[ Ilrm ] = QS[ 4,Ilrm ] - QS[ 1,Ilrm ];
                Sl = Sl + Math.Pow( E2[ Ilrm ],2.0 );
            }
            Sl = Math.Sqrt( Sl );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { E2[ Ilrm ] = E2[ Ilrm ] / Sl; }
            F1[ 1 ] = E2[ 2 ];
            F1[ 2 ] = -E2[ 1 ];
            F1[ 3 ] = 0.0;
            VECPR( 3,E2,F1,ref F2,ref Sl );
            double COSD = Math.Cos( DIPPLS[ 4 ] );
            double SIND = Math.Sin( DIPPLS[ 4 ] );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { E1[ Ilrm ] = COSD * F1[ Ilrm ] + SIND * F2[ Ilrm ]; }
            VECPR( 3,E1,E2,ref F1,ref Sl );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { SNQ[ 4,Ilrm ] = F1[ Ilrm ]; }
            QS[ 8,1 ] = 0.0;
            QS[ 8,2 ] = 0.0;
            QS[ 8,3 ] = -H2;
            SNQ[ 8,1 ] = 0.0;
            SNQ[ 8,2 ] = 0.0;
            SNQ[ 8,3 ] = 1.0;
            SECPLNS( 2,2,8,2,5,8,ref PS );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { QS[ 6,Ilrm ] = PS[ Ilrm ]; }
            SECPLNS( 1,6,2,4,3,5,ref PS );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { QS[ 5,Ilrm ] = PS[ Ilrm ]; }
            SECPLNS( 3,2,6,6,2,3,ref PS );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { QS[ 7,Ilrm ] = PS[ Ilrm ]; }
            SECPLNS( 3,5,1,6,3,4,ref PS );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { QS[ 8,Ilrm ] = PS[ Ilrm ]; }
            for(Ilrm = 1; Ilrm <= 3; Ilrm++) { PRPD[ 4,Ilrm ] = QS[ 1,Ilrm ]; }
            S12 = 0.0;
            S14 = 0.0;
            S15 = 0.0;
            for(Ilrm = 1; Ilrm <= 3; Ilrm++)
            {
                E[ 1,Ilrm ] = QS[ 2,Ilrm ] - QS[ 1,Ilrm ];
                S12 = S12 + Math.Pow( E[ 1,Ilrm ],2.0 );
                E[ 2,Ilrm ] = QS[ 4,Ilrm ] - QS[ 1,Ilrm ];
                S14 = S14 + Math.Pow( E[ 2,Ilrm ],2.0 );
                E[ 3,Ilrm ] = QS[ 5,Ilrm ] - QS[ 1,Ilrm ];
                S15 = S15 + Math.Pow( E[ 3,Ilrm ],2.0 );
            }
            S12 = Math.Sqrt( S12 );
            S14 = Math.Sqrt( S14 );
            S15 = Math.Sqrt( S15 );
            for(Ilrm = 1; Ilrm <= 3; Ilrm++)
            {
                E[ 1,Ilrm ] = E[ 1,Ilrm ] / S12;
                E[ 2,Ilrm ] = E[ 2,Ilrm ] / S14;
                E[ 3,Ilrm ] = E[ 3,Ilrm ] / S15;
            }
            SECPLNS( 1,1,3,1,5,4,ref PS );
            SECPLNS( 1,1,6,1,5,4,ref E1 );
            SECPLNS( 1,1,7,1,5,4,ref E2 );
            X1 = 0.0;
            X2 = 0.0;
            X3 = 0.0;
            for(Ilrm = 1; Ilrm <= 3; Ilrm++)
            {
                X1 = X1 + E[ 1,Ilrm ] * (PS[ Ilrm ] - QS[ 1,Ilrm ]);
                X2 = X2 + E[ 1,Ilrm ] * (E1[ Ilrm ] - QS[ 1,Ilrm ]);
                X3 = X3 + E[ 1,Ilrm ] * (E2[ Ilrm ] - QS[ 1,Ilrm ]);
            }
            double[] ff = new double[ 4 ];
            ff[ 0 ] = S12; ff[ 1 ] = X1; ff[ 2 ] = X1; ff[ 3 ] = X3;
            double x_im; long i;
            for(i = 0,x_im = ff[ 0 ]; i < 4; i++)
            {
                if(ff[ i ] > x_im) x_im = ff[ i ];
            }
            F1[ 1 ] = x_im;
            //	F1(1)=AMAX1(S12,X1,X2,X3);
            SECPLNS( 1,1,3,4,1,5,ref PS );
            SECPLNS( 1,1,7,4,1,5,ref E1 );
            SECPLNS( 1,1,8,4,1,5,ref E2 );
            X1 = 0.0;
            X2 = 0.0;
            X3 = 0.0;
            for(Ilrm = 1; Ilrm <= 3; Ilrm++)
            {
                X1 = X1 + E[ 2,Ilrm ] * (PS[ Ilrm ] - QS[ 1,Ilrm ]);
                X2 = X2 + E[ 2,Ilrm ] * (E1[ Ilrm ] - QS[ 1,Ilrm ]);
                X3 = X3 + E[ 2,Ilrm ] * (E2[ Ilrm ] - QS[ 1,Ilrm ]);
            }
            ff[ 0 ] = S14; ff[ 1 ] = X1; ff[ 2 ] = X1; ff[ 3 ] = X3;
            for(i = 0,x_im = ff[ 0 ]; i < 4; i++)
            {
                if(ff[ i ] > x_im) x_im = ff[ i ];
            }
            F1[ 2 ] = x_im;
            //	F1(2)=AMAX1(S14,X1,X2,X3)
            SECPLNS( 1,1,6,4,5,1,ref PS );
            SECPLNS( 1,1,7,4,5,1,ref E1 );
            SECPLNS( 1,1,8,4,5,1,ref E2 );
            X1 = 0.0;
            X2 = 0.0;
            X3 = 0.0;
            for(Ilrm = 1; Ilrm <= 3; Ilrm++)
            {
                X1 = X1 + E[ 3,Ilrm ] * (PS[ Ilrm ] - QS[ 1,Ilrm ]);
                X2 = X2 + E[ 3,Ilrm ] * (E1[ Ilrm ] - QS[ 1,Ilrm ]);
                X3 = X3 + E[ 3,Ilrm ] * (E2[ Ilrm ] - QS[ 1,Ilrm ]);
            }
            ff[ 0 ] = S15; ff[ 1 ] = X1; ff[ 2 ] = X1; ff[ 3 ] = X3;
            for(i = 0,x_im = ff[ 0 ]; i < 4; i++)
            {
                if(ff[ i ] > x_im) x_im = ff[ i ];
            }
            F1[ 3 ] = x_im;
            //	F1(3)=AMAX1(S15,X1,X2,X3)
            for(Ilrm = 1; Ilrm <= 3; Ilrm++)
            {
                for(Jlrm = 1; Jlrm <= 3; Jlrm++)
                    PRPD[ Ilrm,Jlrm ] = E[ Ilrm,Jlrm ] * F1[ Ilrm ];
            }
        }

        private void DETT(double[] A,double[] B,double[] C,ref double P)
        {
            P = A[ 1 ] * (B[ 2 ] * C[ 3 ] - B[ 3 ] * C[ 2 ]) - B[ 1 ] * (A[ 2 ] * C[ 3 ] - A[ 3 ] * C[ 2 ]) + C[ 1 ] * (A[ 2 ] * B[ 3 ] - A[ 3 ] * B[ 2 ]);
        }

        private void SECPLNS(long N1,long N2,long N3,long M1,long m2,long M3,ref double[] PS)
        {
            double[] SN1 = new double[ 4 ],
            SN2 = new double[ 4 ],
            SN3 = new double[ 4 ],
            Q1 = new double[ 4 ],
            Q2 = new double[ 4 ],
            Q3 = new double[ 4 ],
            B = new double[ 4 ],
            t = new double[ 4 ];

            double D = 0f;
            for(int Isec = 1; Isec <= 3; Isec++)
            {
                Q1[ Isec ] = QS[ N1,Isec ];
                SN1[ Isec ] = SNQ[ M1,Isec ];
                Q2[ Isec ] = QS[ N2,Isec ];
                SN2[ Isec ] = SNQ[ m2,Isec ];
                Q3[ Isec ] = QS[ N3,Isec ];
                SN3[ Isec ] = SNQ[ M3,Isec ];
            }
            DETT( SN1,SN2,SN3,ref D );
            EPS = 1.0e-6;
            if(Math.Abs( D ) < EPS)
                EmExit( "Ошибка в субмодуле SECPLNS [DET=0]" );
            else
            {
                SCPR( 3,SN1,Q1,ref B[ 1 ],ref UG );
                SCPR( 3,SN2,Q2,ref B[ 2 ],ref UG );
                SCPR( 3,SN3,Q3,ref B[ 3 ],ref UG );
                PS[ 1 ] = B[ 1 ] * (SN2[ 2 ] * SN3[ 3 ] - SN2[ 3 ] * SN3[ 2 ]) - B[ 2 ] * (SN1[ 2 ] * SN3[ 3 ] - SN1[ 3 ] * SN3[ 2 ]) + B[ 3 ] * (SN1[ 2 ] * SN2[ 3 ] - SN1[ 3 ] * SN2[ 2 ]);
                PS[ 2 ] = -B[ 1 ] * (SN2[ 1 ] * SN3[ 3 ] - SN2[ 3 ] * SN3[ 1 ]) + B[ 2 ] * (SN1[ 1 ] * SN3[ 3 ] - SN1[ 3 ] * SN3[ 1 ]) - B[ 3 ] * (SN1[ 1 ] * SN2[ 3 ] - SN1[ 3 ] * SN2[ 1 ]);
                PS[ 3 ] = B[ 1 ] * (SN2[ 1 ] * SN3[ 2 ] - SN2[ 2 ] * SN3[ 1 ]) - B[ 2 ] * (SN1[ 1 ] * SN3[ 2 ] - SN1[ 2 ] * SN3[ 1 ]) + B[ 3 ] * (SN1[ 1 ] * SN2[ 2 ] - SN1[ 2 ] * SN2[ 1 ]);

                for(int Isec = 1; Isec <= 3; Isec++)
                    PS[ Isec ] = PS[ Isec ] / D;
                SCPR( 3,SN1,PS,ref t[ 1 ],ref UG );
                SCPR( 3,SN2,PS,ref t[ 2 ],ref UG );
                SCPR( 3,SN3,PS,ref t[ 3 ],ref UG );

            }
        }

        double FLAZ(double xb,double yb,double xe,double ye,ref double rzl,ref double raz)
        {
            double r = Math.Pow( (xb - xe),2.0 ) + Math.Pow( (yb - ye),2.0 );
            rzl = Math.Sqrt( r );
            ER[ 1 ] = (xe - xb) / rzl;
            ER[ 2 ] = (ye - yb) / rzl;
            raz = Math.Acos( ER[ 2 ] );
            if(ER[ 1 ] < 0.0)
                raz = 2.0 * PI - raz;
            return 1;
        }
        /*
        long PREPAREF(long &IND,long &INMAX,double *MGNT,double &c1,double &c2,double *HA,long &KDEP,
					double &DPTHU, double &DPTHL,double &AKDIP,double &x1,double &y1,double &x2,
					double &y2, double *ER,double *FL,double *SL,double *SW,double *CB,double *CE,
					long *NSEG, long ss,long &INMA,long &KPNTR)*/
        long PREPAREF(double x1,double y1,double x2,double y2,long ss)
        {
            double c1 = PRT[ 1 ],c2 = PRT[ 2 ];
            double SMAG,CS,SA,CLI,CLD,CD0,FLMT,CLD1,CLI1,SL0,SW0,FLSLR,FL1,RAT,RAT0;
            ALBYW = 1.0;
            double C11,C21,KTD1,RHS,H0U0,H0L0,DIPF1,KTD2;
            double[] XB0 = new double[ 3 ],XE0 = new double[ 3 ];
            long j,i,kj,k1;
            XB0[ 1 ] = x1;
            XB0[ 2 ] = y1;
            XE0[ 1 ] = x2;
            XE0[ 2 ] = y2;
            FL0 = Math.Sqrt( Math.Pow( (XE0[ 1 ] - XB0[ 1 ]),2.0 ) + Math.Pow( (XE0[ 2 ] - XB0[ 2 ]),2.0 ) );
            ER[ 1 ] = (XE0[ 1 ] - XB0[ 1 ]) / FL0;
            ER[ 2 ] = (XE0[ 2 ] - XB0[ 2 ]) / FL0;
            CS = c1 + c2;
            INMA = INMAX;
            for(i = 1; i <= INMAX; i++)
            {
                SMAG = MGNT[ i ];
                SA = Math.Pow( 10,(SMAG - CMAG) );
                if(SMAG < CMW1)
                    ALBYW = CLW1;
                if(SMAG < CMW2 && SMAG >= CMW1)
                    ALBYW = CLW1 + (CLW2 - CLW1) * (SMAG - CMW1) / (CMW2 - CMW1);
                if(SMAG >= CMW2)
                    ALBYW = CLW2;
                SL[ i ] = Math.Sqrt( SA * ALBYW );
                SW[ i ] = SA / SL[ i ];
            }
            if(KPNTR == 0 || KPNTR == 1)
            {
                for(i = 1; i <= INMAX; i++)
                {
                    XB[ i,1 ] = XB0[ 1 ] + (.5 - c1) * SL[ i ] * ER[ 1 ];
                    XB[ i,2 ] = XB0[ 2 ] + (.5 - c1) * SL[ i ] * ER[ 2 ];
                    FL[ i ] = FL0 + (CS - 1.0) * SL[ i ];
                    if(FL[ i ] <= 0.0)
                    {
                        wrng_out.WriteLine( "Зона " + IND );
                        wrng_out.WriteLine( "Длина разлома мала для очагов магнитуды >= " + MGNT[ i ] );
                        wrng_out.WriteLine( "магнитуды равные и превышающие " + MGNT[ i ] + " исключаются" );
                        INMA = i - 1;
                        if(INMA == 0)
                        {
                            str = String.Format( "зона {0} исключается",IND );
                            outFile.WriteLine( str );
                            wrng_out.WriteLine( str );
                            return ss;
                        } else
                            goto a13;
                    }
                }
            } else if(KPNTR == 2)
            {
                CLI = 0.0;
                CLD = 0.0;
                CD0 = 1.0 / 3.0;
                FLMT = (1.0 - CS) * (1.0 - CD0);
                for(i = 1; i <= INMAX; i++)
                {
                    CLD1 = 0.0;
                    CLI1 = 0.0;
                    SL0 = SL[ i ];
                    SW0 = SW[ i ];
                    SA = SL0 * SW0;
                    if(FL0 < (SL0 * FLMT))
                    {
                        FLSLR = FL0 / SL0;
                        wrng_out.WriteLine( "магнитуда и длина разлома не согласуются: FL0/SL0<FLMT" );
                        wrng_out.WriteLine( "     MAG      L0    FL        C1      C2     CDm   FL0/SL0   FLMT" );
                        str = String.Format( "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t",MGNT[ i ],SL0,FL0,c1,c2,CD0,FLMT,FLSLR );
                        wrng_out.WriteLine( str );
                        INMA = i - 1;
                        if(INMA == 0)
                        {
                            str = String.Format( "зона {0} исключается",IND );
                            outFile.WriteLine( str );
                            wrng_out.WriteLine( str );
                            return ss;
                        } else
                        {
                            outFile.WriteLine( "Зона " + IND );
                            wrng_out.WriteLine( "Зона " + IND );
                            outFile.WriteLine( "верхние магнитуды исключаются, начиная с " + MGNT[ i ] );
                            outFile.WriteLine( "магнитуды равные и превышающие " + MGNT[ i ] + " исключаются" );
                            goto a13;
                        }
                    }
                    FL1 = FL0 + SL0 * CS;
                    RAT = FL0 / SL0;
                    kj = (long)(RAT);
                    RAT0 = 2.0 * kj * (kj + 1 - CS) / (2.0 * kj + 1 - CS);
                    k1 = Convert.ToInt32( FL1 / SL0 );
                    if(k1 > kj)
                    {
                        FL[ i ] = SL0 * (kj + 1);
                        SL[ i ] = SL0;
                        NSEG[ i ] = kj + 1;
                    } else if(RAT <= RAT0)
                    {
                        FL[ i ] = FL0;
                        SL[ i ] = FL[ i ] / kj;
                        NSEG[ i ] = kj;
                        CLI1 = (SL[ i ] - SL0) / SL0;
                        if(CLI1 > CLI) { CLI = CLI1; }
                    } else
                    {
                        SL[ i ] = FL0 / (kj + 1 - CS);
                        FL[ i ] = FL0 + CS * SL[ i ];
                        NSEG[ i ] = kj + 1;
                        CLD1 = (SL0 - SL[ i ]) / SL0;
                        if(CLD1 > CLD) { CLD = CLD1; }
                    }
                    if(CS == 0.0)
                    {
                        C11 = 0.0;
                        C21 = 0.0;
                    } else
                    {
                        C11 = (FL[ i ] - FL0) / CS;
                        C21 = C11 * c2;
                        C11 = C11 * c1;
                    }
                    for(j = 1; j <= 2; j++) { XB[ i,j ] = XB0[ j ] - C11 * ER[ j ]; }
                    CB[ i ] = C11 / SL[ i ];
                    CE[ i ] = C21 / SL[ i ];
                    SW[ i ] = SA / SL[ i ];
                }
            } else
            {
                FL[ 1 ] = FL0;
                FL[ 2 ] = c1;
                FL[ 3 ] = CS;
                for(i = 1; i <= INMAX; i++)
                {
                    XB[ i,1 ] = XB0[ 1 ];
                    XB[ i,2 ] = XB0[ 2 ];
                    if(FL0 < ((1.0 - CS) * SL[ i ]))
                    {
                        wrng_out.WriteLine( "Зона " + IND );
                        wrng_out.WriteLine( "Длина разлома мала для очагов магнитуды >= " + MGNT[ i ] );
                        wrng_out.WriteLine( "магнитуды равные и превышающие " + MGNT[ i ] + " исключаются" );
                        INMA = i - 1;
                        if(INMA == 0)
                        {
                            str = String.Format( "зона {0} исключается",IND );
                            outFile.WriteLine( str );
                            wrng_out.WriteLine( str );
                            return ss;
                        } else goto a13;
                    }
                }
            }
a13:
            if(KDEP == 1)
            {
                for(i = 1; i <= INMA; i++)
                {
                    KTD1 = 0;
                    if(PRT1 != 0.0)
                    {
                        RHS = SW[ i ] * Math.Sin( DPS1 );
                        H0U0 = DPTHU + (.5 + AKDIP) * RHS;
                        if(HA[ 1 ] > H0U0) H0U0 = HA[ 1 ];
                        H0L0 = DPTHL - (.5 - AKDIP) * RHS;
                        if(HA[ 2 ] < H0L0) H0L0 = HA[ 2 ];
                        if(H0U0 > H0L0)
                        {
                            wrng_out.WriteLine( "Зона " + IND );
                            DIPF1 = DPS1 * 180.0 / (4.0 * Math.Atan( 1.0 ));
                            wrng_out.WriteLine( "Условия на глубины противоречивы для 1-го угла падения " + DIPF1 );
                            wrng_out.WriteLine( "      MAG    H1     H2     DPTHU   DPTHL     W      DIP     H0U     H0L" );
                            str = String.Format( "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",MGNT[ i ],HA[ 1 ],HA[ 2 ],DPTHU,DPTHL,SW[ i ],DIPF1,H0U0,H0L0 );
                            wrng_out.WriteLine( str );
                            KTD1 = 1;
                        }
                    }
                    KTD2 = 0;
                    if(PRT2 != 0.0)
                    {
                        RHS = SW[ i ] * Math.Sin( DPS2 );
                        H0U0 = DPTHU + (.5 + AKDIP) * RHS;
                        if(HA[ 1 ] > H0U0) H0U0 = HA[ 1 ];
                        H0L0 = DPTHL - (.5 - AKDIP) * RHS;
                        if(HA[ 2 ] < H0L0) H0L0 = HA[ 2 ];
                        if(H0U0 > H0L0)
                        {
                            wrng_out.WriteLine( "Зона " + IND );
                            DIPF1 = DPS2 * 180.0 / (4.0 * Math.Atan( 1.0 ));
                            wrng_out.WriteLine( "Условия на глубины противоречивы для 2-го угла падения " + DIPF1 );
                            wrng_out.WriteLine( "      MAG    H1     H2     DPTHU   DPTHL     W      DIP     H0U     H0L" );
                            str = String.Format( "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}",MGNT[ i ],HA[ 1 ],HA[ 2 ],DPTHU,DPTHL,SW[ i ],DIPF1,H0U0,H0L0 );
                            wrng_out.WriteLine( str );
                            KTD2 = 1;
                        }
                    }
                    if(KTD1 == 0 || KTD2 == 0) goto a12;
                    INMA = i - 1;
                    if(INMA == 0)
                    {
                        wrng_out.WriteLine( "Зона " + IND + " исключается" );
                        return ss;
                    } else
                    {
                        wrng_out.WriteLine( String.Format( "верхние магнитуды, начиная с {0} исключаются",MGNT[ i ] ) );
                        goto a3;
                    }
a12: ;
                }
            }
a3:
            return 0;
        }

        private void RSKVN(double PNUM,double[] S,double[] S1,ref double X1)
        {
            int ie1 = 1,ie;
            double X0;
            SUM = 0.0;
            for(ie = 1; ie <= IMGS; ie++) { SUM += S[ ie ] * S[ ie ]; }
            for(ie = 1; ie <= IMGS; ie++) { S1[ ie ] = S[ ie ] / PNUM; }
            if(SUM < .00001)
                X1 = 1.0;
            else
            {
                X0 = 1.0;
                for(ie = 1; ie <= IMGS; ie++)
                {
                    if(S1[ ie ] > X0)
                        continue;
                    ie1 = ie;
                    break;
                }
                if(ie1 == 1)
                { X1 = 2.0; return; } else
                    X1 = GI0 + DI * (ie1 - 2) + DI * (S1[ ie1 - 1 ] - 1.0) / (S1[ ie1 - 1 ] - S1[ ie1 ]);
            }
        }

        //Можно удалить
        private void DeagregForRS(double PGA_SA,  double[,] PGA_SA_deagreg, int sk, long ideg, int Num)
        {
            if (PGA_SA < PGA_SA_deagreg[sk,0]) { }
            else  ////////////////////деагрегация
            {
                jdeg = (int)(R3D / 5);
                if (jdeg < 71)
                {
                    DEAGREGRespSpectr[Num, (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                }
            }

            if (PGA_SA < PGA_SA_deagreg[sk, 1]) { }
            else  ////////////////////деагрегация
            {
                jdeg = (int)(R3D / 5);
                if (jdeg < 71)
                {
                    DEAGREGRespSpectr[(Num+1), (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                }
            }

            if (PGA_SA < PGA_SA_deagreg[sk, 2]) { }
            else  ////////////////////деагрегация
            {
                jdeg = (int)(R3D / 5);
                if (jdeg < 71)
                {
                    DEAGREGRespSpectr[(Num + 2), (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                }
            }

            if (PGA_SA < PGA_SA_deagreg[sk, 3]) { }
            else  ////////////////////деагрегация
            {
                jdeg = (int)(R3D / 5);
                if (jdeg < 71)
                {
                    DEAGREGRespSpectr[(Num + 3), (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                }
            }

            if (PGA_SA < PGA_SA_deagreg[sk, 4]) { }
            else  ////////////////////деагрегация
            {
                jdeg = (int)(R3D / 5);
                if (jdeg < 71)
                {
                    DEAGREGRespSpectr[(Num + 4), (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                }
            }

            if (PGA_SA < PGA_SA_deagreg[sk, 5]) { }
            else  ////////////////////деагрегация
            {
                jdeg = (int)(R3D / 5);
                if (jdeg < 71)
                {
                    DEAGREGRespSpectr[(Num + 5), (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                }
            }

            if (PGA_SA < PGA_SA_deagreg[sk, 6]) { }
            else  ////////////////////деагрегация
            {
                jdeg = (int)(R3D / 5);
                if (jdeg < 71)
                {
                    DEAGREGRespSpectr[(Num + 6), (ideg - 1) * 70 + jdeg + 1 + (sk - 1) * 770]++;
                }
            }
        }
    }
}
