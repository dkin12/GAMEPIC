using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;

namespace Fungus    // 플로우차트 쓸라면 필요함
{
    public class ClueManager : MonoBehaviour
    {
        // 플로우 차트에서 받아올 변수
        public GameObject gameObject;
        public Flowchart flowChart;
        public int dayNum;
        public int clueNum;

        // 단서 데이터, 단서 페이지에 필요한 변수
        Dictionary<int, string[]> clueData = new Dictionary<int, string[]>();
        public string[] clueValue = new string[6];      // 딕셔너리에 있는 value 임시로 받을 변수
        public string[] leftPageTxt = new string[15];   // Day에 대한 내용 담을 변수
        public string[][] rightPageTxt = new string[15][];  // 단서에 대한 내용 담을 변수
        public Text leftUIText; // Day에 대한 내용을 보여줄 UI
        public Text rightUIText;    // 단서에 대한 내용 보여줄 UI
        public bool addedClue = false;  // 단서 중복 추가 방지 변수



        // Start is called before the first frame update
        void Start()
        {
            // 변수값 초기화
            gameObject = GameObject.Find("Start_day1");
            flowChart = gameObject.GetComponent<Flowchart>();
            dayNum = flowChart.GetIntegerVariable("dayNum");
            clueNum = flowChart.GetIntegerVariable("clueNum");
            leftUIText = GameObject.Find("Date").GetComponent<Text>();
            rightUIText = GameObject.Find("Clues").GetComponent<Text>();
            // 단서 데이터 저장
            GenerateData();
            //Memoing();
            //clueData.TryGetValue(dayNum, out clueValue);
            //Debug.Log(clueValue[clueNum]);
        }

        // Update is called once per frame
        void Update()
        {
            // 변수값 업데이트
            dayNum = flowChart.GetIntegerVariable("dayNum");
            // 단서 추가 여부 확인
            if (clueNum != (clueNum = flowChart.GetIntegerVariable("clueNum")))
                addedClue = false;
            // 단서는 한번만 추가
            if (!addedClue)
            {
                Memoing();   // 고쳤다..?
                addedClue = true;
            }
        }

        // 딕셔너리에 데이별 단서내용 저장
        void GenerateData()
        {
            clueData.Add(1, new string[] {"유리아와 데이비드 싸움",
            "데이비드와 제이크는 붙어다님",
            "유리아 - 돈 많음 데이비드 - 귀족 집안" ,
            "존이 유리아를 좋아함",
            "데이비드는 여자를 안 좋아한다?",
            "존은 여름 밤마다 얼음을 배달함"
             });
            clueData.Add(2, new string[] {"베르모트 저택 경비원들 다녀감(주기적으로 오는 듯)",
        "베르모트 저택 시녀 올리비아 다녀감(기분 안 좋아보임)"
    });//2

            clueData.Add(3, new string[] {"베르모트는 예전에 갑질 등으로 원망을 많이 삼",
        "집사 중 하나는 존의 아버지였다는 얘기도..."
    });//2

            clueData.Add(4, new string[] {"유리아는 날카로운 것으로 목을 찔려 사망함",
        "고소공포증으로 유리아의 방은 1층이었음"
    });//2
            clueData.Add(5, new string[] { "단서가 될 정보가 없습니다." });

            clueData.Add(6, new string[] {"제이크와 데이비드는 연인 사이",
        "베르모트 위중한 상태",
        "유리아가 베르모트보다 먼저 사망 시 재산은 데이비드에게 감"
    });//3

            clueData.Add(7, new string[] {"유리아 방 카페트는 와인색",
        "사건 현장에서 데이비드의 피 묻은 손수건 발견됨"
    });//2

            clueData.Add(8, new string[] { "단서가 될 정보가 없습니다." });

            clueData.Add(9, new string[] {"시녀 올리비아가 유리아에게 앙심을 품고 있었음"
    });//1

            clueData.Add(10, new string[] {"올리비아가 당일 밤 유리아 방 앞을 어슬렁거리는 걸 본 목격자가 존재",
        "올리비아 :해치러 간 건 맞지만 문이 잠겨 있어서 시도도 못 해보고 돌아왔어요"
    });//2

            clueData.Add(11, new string[] {"데이비드 애인 제이크가 유리아를 질투해 살해하고, 재산을 데이비드에게 남겨지게 한다는 가설"
    });//1

            clueData.Add(12, new string[] { "단서가 될 정보가 없습니다." });

            clueData.Add(13, new string[] {"올리비아가 다시 가 본 유리아의 방 문은 잠겨있지 않았다",
        "카펫에는 그동안 보지 못한 얼룩이 있었다"
    });//2

            clueData.Add(14, new string[] {"처음부터 문이 잠겨있지 않았을 수도..?"
    });//1

            clueData.Add(15, new string[] { "단서가 될 정보가 없습니다." });

        }

        // UI에 보여질 내용을 변수에 미리 저장하고 보여주기까지하게 만들어버렸다..
        void Memoing()
        {
            clueData.TryGetValue(dayNum, out clueValue);
            switch (dayNum)
            {
                case 1:
                    //leftPageTxt[0] = "Day1";
                    leftUIText.text = "Day1";
                    rightPageTxt[0] = clueValue;
                    rightUIText.text += rightPageTxt[0][clueNum] + "\n";
                    break;
                case 2:
                    leftPageTxt[1] = "Day2";
                    rightPageTxt[1][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 3:
                    leftPageTxt[2] = "Day3";
                    rightPageTxt[2][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 4:
                    leftPageTxt[3] = "Day4";
                    rightPageTxt[3][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 5:
                    leftPageTxt[4] = "Day5";
                    rightPageTxt[4][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 6:
                    leftPageTxt[5] = "Day6";
                    rightPageTxt[5][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 7:
                    leftPageTxt[6] = "Day7";
                    rightPageTxt[6][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 8:
                    leftPageTxt[7] = "Day8";
                    rightPageTxt[7][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 9:
                    leftPageTxt[8] = "Day9";
                    rightPageTxt[8][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 10:
                    leftPageTxt[9] = "Day10";
                    rightPageTxt[9][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 11:
                    leftPageTxt[10] = "Day11";
                    rightPageTxt[10][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 12:
                    leftPageTxt[11] = "Day12";
                    rightPageTxt[11][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 13:
                    leftPageTxt[12] = "Day13";
                    rightPageTxt[12][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 14:
                    leftPageTxt[13] = "Day14";
                    rightPageTxt[13][clueNum] += clueValue[clueNum] + "\n";
                    break;
                case 15:
                    leftPageTxt[14] = "Day15";
                    rightPageTxt[14][clueNum] += clueValue[clueNum] + "\n";
                    break;
                default:
                    break;
            }


        }

        void ShowClueToBook()
        {
            // 왼쪽 페이지 보여주기
            
        
            leftUIText.text = "Day1";   // 이렇게하면 왼쪽페이지에 Day1이라는 글자가 뜸
            leftUIText.text = leftPageTxt[0];   // 이렇게하면 왼쪽페이지에 아무것도 안뜸
            // 아무래도  Memoing()에서 pass by reference가 아니라 pass by value로 넣어서 그런듯?

           // rightUIText.text += rightPageTxt[0][clueNum] + "\n";
            //
            // 오른쪽 페이지 보여주기
            //for(int i = 0; i < rightPageTxt[dayNum-1].Length; i++)
           // {
            //    rightUIText.text += rightPageTxt[dayNum - 1][i] + "\n";
            //}
        }

        
    }
    
}
