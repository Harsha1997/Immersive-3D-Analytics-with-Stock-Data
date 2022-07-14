using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LineGraph1_2 : MonoBehaviour
{
    [Header("Graph Objects")]
    [SerializeField]
    private RectTransform _graphContainer;

    [SerializeField]
    private RectTransform _tempValueX,
        _tempValueY;

    //[SerializeField]
    //private RectTransform _tempGridlineX,
    //    _tempGridlineY;

    private float _graphWidth,
        _graphHeight;

    [Header("Graph Data")]
    [SerializeField]
    private Sprite _dataPointSprite;
    [SerializeField]
    private Vector2 _dataPointSize = new Vector2(15f, 15f);
    [SerializeField]
    public List<DataPoint> _dataList = new List<DataPoint>();




    private List<DataPoint> _dataToGraphList = new List<DataPoint>();
    private DateTime _selectionDate;

    private DateTime _selectedDate;

    double[] stockValues = { 157.425003, 162.994995, 163.9249955, 164.2850035, 165.154999, 167.2300035, 163.8250045, 162.0299985, 159.340004, 161.410004, 158.1849975, 156.8899995, 152.1100005, 152.975006, 157.2300035, 159.3150025, 162.1199955, 164.6800005, 167.165001, 170.1449965, 172.175003, 174.0149995, 173.864998, 177.6749955, 178.154999, 176.2149965, 173.4100035, 176.4650035, 176.3600005, 171.880005 };
    List<double> stockAxis = new List<double> { 192.870003, 201.5850065, 202.029999, 198.2149965, 197.2450025, 193.6050035, 183.645004, 175.8899995, 174.2000045, 180.1399995, 178.7050015, 180.404999, 175.4000015, 178.050003, 185.9500045, 187.7550045, 191.7400055, 183.9400025, 188.6900025, 187.5950015, 186.9550015, 189.314995, 186.864998, 193.779999, 194.404999, 194.5149995, 190.699997, 189.375, 187.435005, 178.5750045, 157.425003, 162.994995, 163.9249955, 164.2850035, 165.154999, 167.2300035, 163.8250045, 162.0299985, 159.340004, 161.410004, 158.1849975, 156.8899995, 152.1100005, 152.975006, 157.2300035, 159.3150025, 162.1199955, 164.6800005, 167.165001, 170.1449965, 172.175003, 174.0149995, 173.864998, 177.6749955, 178.154999, 176.2149965, 173.4100035, 176.4650035, 176.3600005, 171.880005 }; string[] dateValues = { "2/24/2022", "2/25/2022", "2/28/2022", "3/1/2022", "3/2/2022", "3/3/2022", "3/4/2022", "3/7/2022", "3/8/2022", "3/9/2022", "3/10/2022", "3/11/2022", "3/14/2022", "3/15/2022", "3/16/2022", "3/17/2022", "3/18/2022", "3/21/2022", "3/22/2022", "3/23/2022", "3/24/2022", "3/25/2022", "3/28/2022", "3/29/2022", "3/30/2022", "3/31/2022", "4/1/2022", "4/4/2022", "4/5/2022", "4/6/2022" };
    List<string> _timePeriods = new List<string>();





    /*
    private DateTime[] _timePeriods = new DateTime[]
    {
        new DateTime(2001, 01, 01, 0, 0, 0), // Midnight
        new DateTime(2001, 01, 01, 4, 0, 0), // 4 AM
        new DateTime(2001, 01, 01, 8, 0, 0), // 8 AM
        new DateTime(2001, 01, 01, 12, 0, 0), // 12 PM
        new DateTime(2001, 01, 01, 16, 0, 0), // 4 PM
        new DateTime(2001, 01, 01, 20, 0, 0), // 8 PM
        new DateTime(2001, 01, 01, 0, 0, 0) // Midnight
    };
    */
    [Header("Axes Values")]
    [SerializeField]
    private XAxis _xAxis;
    [SerializeField]
    private YAxis _yAxis;

    private DateTime _dataPointTimeStamp;
    private float _dataPointMinutes;
    private float _totalMinutes;

    private List<GameObject> _graphedObjList = new List<GameObject>();

    private GameObject _lastDataPoint;
    private GameObject _newDataPoint;
    private GameObject _dataPointObj;
    private RectTransform _dataPointRect;
    private Vector2 _dataPosition;

    private GameObject _dataConnector;
    private GameObject _connectorObj;
    private RectTransform _connectorRect;
    private Vector2 _connectorDirection;
    private float _connectorDistance;
    private float _connectorAngle;
    [Space, SerializeField]
    private Color _connectorColor = new Color(1, 0, 0, 1);
    private Color _dataPointColor = new Color(1, 1, 1, 1);

    private Vector2 _defaultVector = Vector2.zero;
    private Vector3 _defaultScale = Vector3.one;


    public Dropdown dropDown1;
    public Dropdown dropDown2;


    public Button button1W;
    public Button button1M;
    public Button button1Y;
    public Button button5Y;


    //string[] _companyNames = {"Amazon","Apple","Meta","Google","Karat"};

    //List<string> _companyNames = new List<string>();

    List<string> _companyNames = new List<string> { "Apple", "Microsoft", "Berkshire Hathaway", "Meta Platforms", "Nvidia", "Walmart", "JP Morgan", "Goldman Sachs", "American Express", "UnitedHealth Group" };
    List<string> _stickers = new List<string> { "AAPL.csv", "MSFT.csv", "BRK-B.csv", "FB.csv", "NVDA.csv", "WMT.csv", "JPM.csv", "GS.csv", "AXP.csv", "UNH.csv" };
    Dictionary<string, List<DataPoint>> stockMapping = new Dictionary<string, List<DataPoint>>();

    void Start()
    {



        dropDown1 = GameObject.Find("Stock1").GetComponent<Dropdown>();
        dropDown2 = GameObject.Find("Stock2").GetComponent<Dropdown>();


        button1W = GameObject.Find("Button1W").GetComponent<Button>();
        button1M = GameObject.Find("Button1M").GetComponent<Button>();
        button1Y = GameObject.Find("Button1Y").GetComponent<Button>();
        button5Y = GameObject.Find("Button5Y").GetComponent<Button>();

        //Debug.Log("Testing button1 "+ button1M.GetComponentInChildren<Text>().text);



        button1W.onClick.AddListener(delegate {
            myDropdownValueChangedHandler(dropDown1, dropDown2, button1W);
        });

        button1M.onClick.AddListener(delegate {
            myDropdownValueChangedHandler(dropDown1, dropDown2, button1M);
        });

        button1Y.onClick.AddListener(delegate {
            myDropdownValueChangedHandler(dropDown1, dropDown2, button1Y);
        });

        button5Y.onClick.AddListener(delegate {
            myDropdownValueChangedHandler(dropDown1, dropDown2, button5Y);
        });



        dropDown1.onValueChanged.AddListener(delegate {
            myDropdownValueChangedHandler(dropDown1, dropDown2, button1W);
        });

        dropDown2.onValueChanged.AddListener(delegate {
            myDropdownValueChangedHandler(dropDown1, dropDown2, button1W);
        });



        dropDown1.options.Clear();
        dropDown2.options.Clear();






        foreach (var stock in _companyNames)
        {
            dropDown1.options.Add(new Dropdown.OptionData() { text = stock });
            dropDown2.options.Add(new Dropdown.OptionData() { text = stock });

        }

        



    }



    public void buttonClickChangeGraph()
    {



        var selectedStockIndex = 0;
        var selectedStockIndex2 = 1;
        string buttonName = "1W";

        var selectedStockDataPoints1 = stockMapping[_companyNames[selectedStockIndex]];
        var selectedStockDataPoints2 = stockMapping[_companyNames[selectedStockIndex2]];
        Debug.Log("Clicked" + buttonName);

        var sesonedStockData1 = new List<DataPoint>();
        var sesonedStockData2 = new List<DataPoint>();

        Debug.Log("Created Count 1 " + sesonedStockData1.Count);
        Debug.Log("Created Count 2 " + sesonedStockData2.Count);
        string week1 = "1W";
        string month1 = "1M";
        string year1 = "1Y";
        if (buttonName.Equals(week1))
        {
            Debug.Log("Clicked 1M" + buttonName);
            sesonedStockData1 = weeklyData(selectedStockDataPoints1);
            sesonedStockData2 = weeklyData(selectedStockDataPoints2);

        }
        else if (buttonName.Equals(month1))
        {
            Debug.Log("Clicked 1M" + buttonName);
            sesonedStockData1 = monthlyData(selectedStockDataPoints1);
            sesonedStockData2 = monthlyData(selectedStockDataPoints2);
        }
        else if (buttonName.Equals(year1))
        {
            Debug.Log("Clicked 1Y" + buttonName);
            sesonedStockData1 = yearlyData(selectedStockDataPoints1);
            sesonedStockData2 = yearlyData(selectedStockDataPoints2);
        }
        else
        {
            Debug.Log("Length of week1" + week1.Length + " length of button=" + buttonName.Length);
            sesonedStockData1 = fiveYearlyData(selectedStockDataPoints1);
            sesonedStockData2 = fiveYearlyData(selectedStockDataPoints2);
        }

 


        Debug.Log("Done Count 1 " + sesonedStockData1.Count);
        Debug.Log("Done  Count 2" + sesonedStockData2.Count);


        //Debug.Log(selectedStockDataPoints1[0]);




        _timePeriods.Clear();
        stockAxis.Clear();


        for (int i = 0; i < sesonedStockData1.Count; i++)
        {
            stockAxis.Add(sesonedStockData1[i].recordedValue);
            _timePeriods.Add(sesonedStockData1[i].timeStamp);
            //print(sesonedStockData1[i].timeStamp);
        }

        for (int i = 0; i < sesonedStockData2.Count; i++)
        {
            stockAxis.Add(sesonedStockData2[i].recordedValue);
        }








        GraphDataSeries(sesonedStockData1);



        Debug.Log("clicked dropDown");


    }





    public void myDropdownValueChangedHandler(Dropdown dropDown1, Dropdown dropDown2, Button button)
    {

        var selectedStockIndex = dropDown1.value;
        var selectedStockIndex2 = dropDown2.value;
        string buttonName = button.GetComponentInChildren<Text>().text;

        var selectedStockDataPoints1 = stockMapping[_companyNames[selectedStockIndex]];
        var selectedStockDataPoints2 = stockMapping[_companyNames[selectedStockIndex2]];
        Debug.Log("Clicked" + buttonName);

        var sesonedStockData1 = new List<DataPoint>();
        var sesonedStockData2 = new List<DataPoint>();

        Debug.Log("Created Count 1 " + sesonedStockData1.Count);
        Debug.Log("Created Count 2 " + sesonedStockData2.Count);
        string week1 = "1W";
        string month1 = "1M";
        string year1 = "1Y";
        if (buttonName.Equals(week1))
        {
            Debug.Log("Length of week1" + week1.Length + " length of button=" + buttonName.Length);
            sesonedStockData1 = weeklyData(selectedStockDataPoints1);
            sesonedStockData2 = weeklyData(selectedStockDataPoints2);

        }
        else if (buttonName.Equals(month1))
        {
            Debug.Log("Clicked 1M" + buttonName);
            sesonedStockData1 = monthlyData(selectedStockDataPoints1);
            sesonedStockData2 = monthlyData(selectedStockDataPoints2);
        }
        else if (buttonName.Equals(year1))
        {
            Debug.Log("Clicked 1Y" + buttonName);
            sesonedStockData1 = yearlyData(selectedStockDataPoints1);
            sesonedStockData2 = yearlyData(selectedStockDataPoints2);
        }
        else
        {
            Debug.Log("Length of week1" + week1.Length + " length of button=" + buttonName.Length);
            sesonedStockData1 = fiveYearlyData(selectedStockDataPoints1);
            sesonedStockData2 = fiveYearlyData(selectedStockDataPoints2);
        }


        Debug.Log("Done Count 1 " + sesonedStockData1.Count);
        Debug.Log("Done  Count 2" + sesonedStockData2.Count);


        //Debug.Log(selectedStockDataPoints1[0]);




        _timePeriods.Clear();
        stockAxis.Clear();


        for (int i = 0; i < sesonedStockData1.Count; i++)
        {
            stockAxis.Add(sesonedStockData1[i].recordedValue);
            _timePeriods.Add(sesonedStockData1[i].timeStamp);
            //print(sesonedStockData1[i].timeStamp);
        }

        for (int i = 0; i < sesonedStockData2.Count; i++)
        {
            stockAxis.Add(sesonedStockData2[i].recordedValue);
        }





        GraphDataSeries(sesonedStockData2);



        Debug.Log("clicked dropDown");

    }



    public void OnEnable()
    {
        createListDataPoint();
        _graphWidth = _graphContainer.sizeDelta.x;
        _graphHeight = _graphContainer.sizeDelta.y;

        //SetXAxisMinMax();

        _selectedDate = new DateTime(2021, 03, 27);// DateTime.Today;

        //CompileGraphData(_selectedDate);
    }

    public void CompileGraphData(DateTime selectedDate)
    {
        for (int i = 0; i < stockValues.Length; i++)
        {
            DataPoint newDatapoint = new DataPoint(dateValues[i], stockValues[i]);
            _dataToGraphList.Add(newDatapoint);

        }
        /*
        DateTime dataTimeStamp = new DateTime(2021, 03, 27, 4, 0, 0);
        int recordedValue = 80;
    
        DataPoint newDatapoint = new DataPoint( dataTimeStamp.ToString(),recordedValue);
        _dataList.Add(newDatapoint);


        dataTimeStamp = new DateTime(2021, 03, 27, 5, 0, 0);
        recordedValue = 5;
        newDatapoint = new DataPoint( dataTimeStamp.ToString(),recordedValue);
        _dataList.Add(newDatapoint);

        dataTimeStamp = new DateTime(2021, 03, 27, 12, 0, 0);
        recordedValue = 80;
        newDatapoint = new DataPoint( dataTimeStamp.ToString(),recordedValue);
        _dataList.Add(newDatapoint);

        foreach (var dataPoint in _dataList)
        {
            _selectionDate = DateTime.Parse(dataPoint.timeStamp);

            if (_selectionDate.Date == selectedDate)
            {
                _dataToGraphList.Add(dataPoint);
            }
        }
        */
        //OrderDataByAscending(_dataToGraphList);
        GraphDataSeries(_dataToGraphList);

    }

    public void OrderDataByAscending(List<DataPoint> dataToGraph)
    {
        var dataSeries = dataToGraph.OrderByDescending(data => DateTime.Parse(data.timeStamp)).Reverse().ToList();

        GraphDataSeries(dataSeries);
    }

    public void GraphDataSeries(List<DataPoint> dataSeries1)
    {
        _graphedObjList.ForEach(obj => Destroy(obj));
        _graphedObjList.Clear();

        SetYAxisMinMax(stockAxis);

        //PlotXAxisLabels();

        //PlotYAxisLabels();

        PlotDataPoints(dataSeries1);

    }



    /*
    private void SetXAxisMinMax()
    {
        _xAxis.minDateTime = _timePeriods[0];
        
        _xAxis.maxDateTime = _timePeriods[5].AddMinutes(240); // take time period before the end of the day and add minutes or else will be viewed as the start of the day 
    }
    */
    public void SetYAxisMinMax(List<double> dataSeries)
    {
        if (dataSeries.Count > 0)
        {
            _yAxis.minValue = (float)dataSeries[0];
            _yAxis.maxValue = (float)dataSeries[0];

            for (int i = 0; i < dataSeries.Count; i++)
            {
                _yAxis.currentValue = (int)dataSeries[i];

                if (_yAxis.currentValue < _yAxis.minValue)
                {
                    _yAxis.minValue = _yAxis.currentValue;
                }

                if (_yAxis.currentValue > _yAxis.maxValue)
                {
                    _yAxis.maxValue = _yAxis.currentValue;
                }
            }

            _yAxis.valueRange = _yAxis.maxValue - _yAxis.minValue;

            if (_yAxis.valueRange <= 0)
            {
                _yAxis.valueRange = 1f;
            }

            _yAxis.minValue -= (_yAxis.valueRange * _yAxis.edgeBuffer);
            _yAxis.maxValue += (_yAxis.valueRange * _yAxis.edgeBuffer);
        }
        else
        {
            _yAxis.minValue = _yAxis.defaultMinValue;
            _yAxis.maxValue = _yAxis.defaultMaxValue;
        }
    }

    public void PlotXAxisLabels()
    {
        _xAxis.minLabelPos = 0f;
        _xAxis.maxLabelPos = 0f;

        _xAxis.labelCount = _timePeriods.Count;

        _xAxis.labelIndex = 0;

        for (int i = 0; i < _xAxis.labelCount; i++)
        {
            // Labels
            _xAxis.currentLabelSpread = _graphWidth / (_xAxis.labelCount + _xAxis.edgeBuffer);
            _xAxis.currentLabelPos = _xAxis.currentLabelSpread + _xAxis.labelIndex * _xAxis.currentLabelSpread;
            //Debug.Log(_xAxis.currentLabelPos);
            if (i == 0)
            {
                _xAxis.minLabelPos = _xAxis.currentLabelPos;
            }
            else if (i == _xAxis.labelCount - 1)
            {
                _xAxis.maxLabelPos = _xAxis.currentLabelPos;
            }

            _xAxis.labelRect = Instantiate(_tempValueX);
            _xAxis.labelRect.SetParent(_graphContainer);
            _xAxis.labelRect.gameObject.SetActive(true);
            _xAxis.labelRect.anchoredPosition = new Vector2(_xAxis.currentLabelPos, _xAxis.labelRect.position.y);
            //Debug.Log(_xAxis.labelRect.GetComponent<TMPro.TextMeshProUGUI>().text);
            _xAxis.labelRect.GetComponent<TMPro.TextMeshProUGUI>().text = _timePeriods[i];
            _xAxis.labelRect.localScale = _defaultScale;
            //Debug.Log(_xAxis.currentLabelPos);
            _graphedObjList.Add(_xAxis.labelRect.gameObject);

            // Gridlines
            /*
            _xAxis.gridlineRect = Instantiate(_tempGridlineX);
            _xAxis.gridlineRect.SetParent(_graphContainer);
            _xAxis.gridlineRect.gameObject.SetActive(true);
            _xAxis.gridlineRect.anchoredPosition = new Vector2(_xAxis.currentLabelPos, _xAxis.gridlineRect.position.y);
            _xAxis.gridlineRect.localScale = _defaultScale;

            _graphedObjList.Add(_xAxis.gridlineRect.gameObject);
            */
            _xAxis.labelIndex++;
        }
    }

    public void PlotYAxisLabels()
    {
        _yAxis.tempLabelCount = _yAxis.labelCount; // Label count set in Inspector based on preference

        if (_yAxis.tempLabelCount > _yAxis.valueRange)
        {
            int addTo(int to)
            {
                return (to % 2 == 0) ? to : (to + 2);
            }

            if (_yAxis.valueRange % 2 != 0)
            {
                _yAxis.tempLabelCount = addTo((int)_yAxis.valueRange);
            }
            else
            {
                _yAxis.tempLabelCount = (int)_yAxis.valueRange;
            }

            if (_yAxis.valueRange == 1)
            {
                _yAxis.tempLabelCount = Mathf.RoundToInt(_yAxis.valueRange) + 3;
                _yAxis.minValue -= 2;
                _yAxis.maxValue += 2;
            }
        }

        for (int i = 0; i <= _yAxis.tempLabelCount; i++)
        {
            _yAxis.labelPosNormal = (i * 1f) / _yAxis.tempLabelCount;

            _yAxis.labelPos = _yAxis.minValue + (_yAxis.labelPosNormal * (_yAxis.maxValue - _yAxis.minValue));

            // Labels
            _yAxis.labelRect = Instantiate(_tempValueY);
            _yAxis.labelRect.SetParent(_graphContainer);
            _yAxis.labelRect.gameObject.SetActive(true);
            _yAxis.labelRect.anchoredPosition = new Vector2(_yAxis.labelRect.position.x, _yAxis.labelPosNormal * _graphHeight);
            _yAxis.labelRect.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(_yAxis.labelPos).ToString();
            _yAxis.labelRect.localScale = _defaultScale;

            _graphedObjList.Add(_yAxis.labelRect.gameObject);

            // Gridlines
            /*
            if (i != 0 && i != _yAxis.tempLabelCount)
            {
                _yAxis.gridlineRect = Instantiate(_tempGridlineY);
                _yAxis.gridlineRect.SetParent(_graphContainer);
                _yAxis.gridlineRect.gameObject.SetActive(true);
                _yAxis.gridlineRect.anchoredPosition = new Vector2(_yAxis.gridlineRect.position.x, _yAxis.labelPosNormal * _graphHeight);
                _yAxis.gridlineRect.localScale = _defaultScale;

                _graphedObjList.Add(_yAxis.gridlineRect.gameObject);

            }
            */
        }
    }

    public void PlotDataPoints(List<DataPoint> dataSeries)
    {
        _lastDataPoint = null;

        //_xAxis.totalTime = TimeSpan.FromTicks(_xAxis.maxDateTime.Ticks - _xAxis.minDateTime.Ticks);

        for (int i = 0; i < dataSeries.Count; i++)
        {
            /*
            _dataPointTimeStamp = DateTime.Parse(dataSeries[i].timeStamp);

            _dataPointMinutes = (float)(_dataPointTimeStamp.TimeOfDay.TotalMinutes - _xAxis.minDateTime.TimeOfDay.TotalMinutes); 

            _totalMinutes = (float)_xAxis.totalTime.TotalMinutes;
            */

            _xAxis.currentLabelSpread = _graphWidth / (_xAxis.labelCount + _xAxis.edgeBuffer);
            _xAxis.currentLabelPos = _xAxis.currentLabelSpread + i * _xAxis.currentLabelSpread;
            //Debug.Log(_xAxis.currentLabelPos);  
            //_xAxis.minMaxLabelVariance = _xAxis.maxLabelPos - _xAxis.minLabelPos;
            //Debug.Log(_xAxis.minLabelPos);
            //_xAxis.graphPos =  (float)((_xAxis.minMaxLabelVariance/60.0)*i);
            //_xAxis.graphPos = (_dataPointMinutes / _totalMinutes) * _xAxis.minMaxLabelVariance + _xAxis.minLabelPos;

            _yAxis.graphPos = (float)((dataSeries[i].recordedValue - _yAxis.minValue) / (_yAxis.maxValue - _yAxis.minValue)) * _graphHeight;

            _dataPosition = new Vector2(_xAxis.currentLabelPos, _yAxis.graphPos);

            _newDataPoint = CreateDataPoint(_dataPosition);

            _graphedObjList.Add(_newDataPoint);

            if (_lastDataPoint != null)
            {
                _dataConnector = CreateDataConnector(_lastDataPoint.GetComponent<RectTransform>().anchoredPosition,
                    _newDataPoint.GetComponent<RectTransform>().anchoredPosition);

                _graphedObjList.Add(_dataConnector);
            }

            _lastDataPoint = _newDataPoint;
        }


    }

    public GameObject CreateDataPoint(Vector2 pos)
    {
        _dataPointObj = new GameObject("Data", typeof(Image));
        _dataPointObj.transform.SetParent(_graphContainer, false);
        _dataPointObj.GetComponent<Image>().sprite = _dataPointSprite;
        _dataPointObj.GetComponent<Image>().color = _dataPointColor;
        _dataPointRect = _dataPointObj.GetComponent<RectTransform>();
        _dataPointRect.anchoredPosition = pos;

        _dataPointRect.sizeDelta = _dataPointSize;
        _dataPointRect.anchorMax = _defaultVector;
        _dataPointRect.anchorMin = _defaultVector;

        return _dataPointObj;
    }

    public GameObject CreateDataConnector(Vector2 pointA, Vector2 pointB)
    {
        _connectorObj = new GameObject("Connection", typeof(Image));
        _connectorObj.transform.SetParent(_graphContainer, false);
        _connectorObj.GetComponent<Image>().color = _connectorColor;

        _connectorDirection = (pointB - pointA).normalized;

        _connectorDistance = Vector2.Distance(pointA, pointB);

        _connectorAngle = Mathf.Atan2(_connectorDirection.y, _connectorDirection.x) * Mathf.Rad2Deg;

        _connectorRect = _connectorObj.GetComponent<RectTransform>();
        _connectorRect.anchoredPosition = pointA + _connectorDirection * _connectorDistance * 0.5f;
        _connectorRect.sizeDelta = new Vector2(_connectorDistance, _yAxis.gridlineWidth);
        _connectorRect.anchorMin = _defaultVector;
        _connectorRect.anchorMax = _defaultVector;
        _connectorRect.localEulerAngles = new Vector3(0, 0, _connectorAngle);

        return _connectorObj;
    }

    public void createListDataPoint()
    {
        for (int i = 0; i < _companyNames.Count; i++)
        {
            stockMapping.Add(_companyNames[i], parseData("../ControllerTest/Assets/data/" + _stickers[i]));
        }

        // Debug.Log(stockMapping["Amazon"]);
    }

    public List<DataPoint> parseData(string path)
    {
        string[] lines = System.IO.File.ReadAllLines(path);
        List<DataPoint> newDataList = new List<DataPoint>();
        // Debug.Log("start="+lines[0]);
        // Debug.Log("second="+lines[1]);
        foreach (string line in lines)
        {
            string[] columns = line.Split(',');
            if (columns[3] == "Low")
                continue;
            string timestamp = columns[0].ToString(); // date
            double stockValue = Convert.ToDouble(columns[3]);// closing value
            DataPoint newDatapoint = new DataPoint(timestamp, stockValue);
            // Debug.Log(timestamp+" "+ columns[3]);
            newDataList.Add(newDatapoint);
        }

        return newDataList;
    }

    public List<DataPoint> weeklyData(List<DataPoint> dataList)
    {
        List<DataPoint> weeklyDataList = new List<DataPoint>();
        int j = dataList.Count - 1;
        for (int i = 0; i < 7; i++)
        {
            weeklyDataList.Add(dataList[j--]);
            // Debug.Log(dataList[j].ToString());
        }
        return weeklyDataList;
    }

    public List<DataPoint> monthlyData(List<DataPoint> dataList)
    {
        List<DataPoint> monthlyDataList = new List<DataPoint>();
        int j = dataList.Count - 1;
        for (int i = 0; i < 30; i++)
        {
            monthlyDataList.Add(dataList[j--]);
        }
        return monthlyDataList;
    }

    public List<DataPoint> yearlyData(List<DataPoint> dataList)
    {
        List<DataPoint> yearlyDataList = new List<DataPoint>();
        int j = dataList.Count - 1;
        for (int i = 0; i < 365 && j >= 0; i += 76)
        {
            yearlyDataList.Add(dataList[j]);
            j -= 7;
        }
        return yearlyDataList;
    }

    public List<DataPoint> fiveYearlyData(List<DataPoint> dataList)
    {
        List<DataPoint> fiveYearlyDataList = new List<DataPoint>();
        int j = dataList.Count - 1;
        for (int i = 0; i < dataList.Count && j >= 0; i += 30)
        {
            fiveYearlyDataList.Add(dataList[j]);
            j -= 30;
        }
        return fiveYearlyDataList;
    }




}


