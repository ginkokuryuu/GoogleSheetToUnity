using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleSheetsToUnity;
using GoogleSheetsToUnity.Utils;

public class GoogleSheetDataHandler : MonoBehaviour
{
    public string sheetId;
    public string worksheetName;
    public GameObject table;

    // Start is called before the first frame update
    void Start()
    {
        SpreadsheetManager.Read(new GSTU_Search(sheetId, worksheetName), HandleData);
    }

    public void HandleData(GstuSpreadSheet spreadsheetRef)
    {
        List<string> allHeader = new List<string>();
        foreach(var headerName in spreadsheetRef.rows[1])
        {
            allHeader.Add(headerName.value);
        }

        int collumnCount = 0;
        foreach(string header in allHeader)
        {
            int rowCount = -1;
            foreach(var row in spreadsheetRef.columns[header])
            {
                rowCount += 1;

                if (rowCount == 0)
                    continue;
                else if (rowCount > 3)
                    break;

                TMPro.TMP_Text cellText = table.transform.GetChild(1).GetChild(rowCount).GetChild(collumnCount).GetComponentInChildren<TMPro.TMP_Text>();
                cellText.text = row.value;

                Debug.Log(row.value);
            }

            collumnCount += 1;
        }

        AddData();
    }

    public void AddData()
    {
        List<string> list1 = new List<string>()
        {
            "A",
            "",
            "C"
        };
        List<string> list2 = new List<string>()
        {
            "",
            "2",
            "3"
        };
        List<List<string>> combined = new List<List<string>>
        {
            list1,
            list2,
        };
        SpreadsheetManager.Append(new GSTU_Search(sheetId, worksheetName), new ValueRange(combined), null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
