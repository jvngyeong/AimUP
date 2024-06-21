using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GraphMaker : MonoBehaviour
{
    public Transform DotGroup;
    public Transform LineGroup;
    public Transform InnerFilledGroup;
    public GameObject Dot;
    public GameObject Line;
    public GameObject MaskPanel;
    public Color DotGreen;
    public Color DotRed;
    public Material BlueMat;
    public Material PurpleMat;
    public RectTransform GraphArea;
    public static float[] AccuracyArray = new float[6];
    private float width;
    private float height;
    public GameObject GameManager;

    private void Awake()
    {
        AccuracyArray[0] = 4;
        AccuracyArray[1] = PlayerPrefs.GetFloat("20Acc") / 25;
        AccuracyArray[2] = PlayerPrefs.GetFloat("30Acc") / 25;
        AccuracyArray[3] = PlayerPrefs.GetFloat("40Acc") / 25;
        AccuracyArray[4] = PlayerPrefs.GetFloat("50Acc") / 25;
        AccuracyArray[5] = PlayerPrefs.GetFloat("60Acc") / 25;
    }
    void Start()
    {
        int CurrentLevel = PlayerPrefs.GetInt("Level");
        int Round = PlayerPrefs.GetInt("Round");

        Debug.Log("0 = " + PlayerPrefs.GetFloat("10Acc"));
        Debug.Log("1 = " + PlayerPrefs.GetFloat("20Acc"));
        Debug.Log("2 = " + PlayerPrefs.GetFloat("30Acc"));
        Debug.Log("3 = " + PlayerPrefs.GetFloat("40Acc"));
        Debug.Log("4 = " + PlayerPrefs.GetFloat("50Acc"));
        Debug.Log("5 = " + PlayerPrefs.GetFloat("60Acc"));
        width = GraphArea.rect.width;
        height = GraphArea.rect.height;
        DrawGraph();

        if (Round <= 4){
            GameManager.SendMessage("LevelBalancing", SendMessageOptions.DontRequireReceiver);
            Round += 1; 
            PlayerPrefs.SetInt("Level",4);
            PlayerPrefs.SetInt("Round",Round);
        }
    }

    private void DrawGraph()
    {
        float startPositionX = -width / 2;
        float maxYPosition = height / 2;
        var innerFilled = new List<Vector3>();
        Vector2 prevDotPos = Vector2.zero;

        for (int i = 0; i < AccuracyArray.Length; i++)
        {
            // Dot
            GameObject dot = Instantiate(Dot, DotGroup, true);
            dot.transform.localScale = Vector3.one;

            RectTransform dotRT = dot.GetComponent<RectTransform>();
            Image dotImage = dot.GetComponent<Image>();

            float yPos = AccuracyArray[i];

            dotImage.color = yPos >= 0f ? DotGreen : DotRed;

            dotRT.anchoredPosition = new Vector2(startPositionX + (width / (AccuracyArray.Length - 1) * i), maxYPosition * yPos);

            innerFilled.Add(dotRT.anchoredPosition);

            // Line
            if (i == 0)
            {
                prevDotPos = dotRT.anchoredPosition;
                continue;
            }

            GameObject line = Instantiate(Line, LineGroup, true);
            line.transform.localScale = Vector3.one;

            RectTransform lineRT = line.GetComponent<RectTransform>();
            Image lineImage = line.GetComponent<Image>();

            float lineWidth = Vector2.Distance(prevDotPos, dotRT.anchoredPosition);
            float xPos = (prevDotPos.x + dotRT.anchoredPosition.x) / 2;
            yPos = (prevDotPos.y + dotRT.anchoredPosition.y) / 2;

            Vector2 dir = (dotRT.anchoredPosition - prevDotPos).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            lineRT.anchoredPosition = new Vector2(xPos, yPos);
            lineRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lineWidth);
            lineRT.localRotation = Quaternion.Euler(0f, 0f, angle);

            GameObject maskPanel = Instantiate(MaskPanel, Vector3.zero, Quaternion.identity);
            maskPanel.transform.SetParent(LineGroup);
            maskPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            maskPanel.transform.SetParent(line.transform);

            prevDotPos = dotRT.anchoredPosition;
        }

        CreateFilledGraphShape(innerFilled.ToArray());
    }

    private void CreateFilledGraphShape(Vector3[] linePoints)
    {
        List<Vector3> filledGraphPointList = new List<Vector3>();
        Vector3 tmp;
        float x;

        for (int i = 0; i < linePoints.Length; ++i)
        {
            filledGraphPointList.Add(new Vector3(linePoints[i].x, 0, 0));
            filledGraphPointList.Add(new Vector3(linePoints[i].x, linePoints[i].y, 0));

            if (i + 1 < linePoints.Length)
            {
                if (linePoints[i].y < 0 && linePoints[i + 1].y >= 0)
                {
                    x = Mathf.Lerp(linePoints[i].x, linePoints[i + 1].x, Mathf.Abs(linePoints[i].y) / (Mathf.Abs(linePoints[i].y) + Mathf.Abs(linePoints[i + 1].y)));
                    tmp = new Vector3(x, 0f, 0f);

                    filledGraphPointList.Add(tmp);

                    MakeRenderer(filledGraphPointList.ToArray(), filledGraphPointList.Count, PurpleMat);

                    filledGraphPointList.Clear();
                    filledGraphPointList.Add(tmp);
                }
                else if (linePoints[i].y > 0 && linePoints[i + 1].y <= 0)
                {
                    x = Mathf.Lerp(linePoints[i].x, linePoints[i + 1].x, Mathf.Abs(linePoints[i].y) / (Mathf.Abs(linePoints[i].y) + Mathf.Abs(linePoints[i + 1].y)));
                    tmp = new Vector3(x, 0f, 0f);

                    filledGraphPointList.Add(tmp);

                    MakeRenderer(filledGraphPointList.ToArray(), filledGraphPointList.Count, BlueMat);

                    filledGraphPointList.Clear();
                    filledGraphPointList.Add(tmp);
                }
            }
        }

        if (filledGraphPointList[filledGraphPointList.Count - 1].y >= 0)
        {
            MakeRenderer(filledGraphPointList.ToArray(), filledGraphPointList.Count, BlueMat);
        }
        else
        {
            MakeRenderer(filledGraphPointList.ToArray(), filledGraphPointList.Count, PurpleMat);
        }
    }

    private void MakeRenderer(Vector3[] graphPoints, int count, Material innerFill)
    {
        int trinangleCount = count - 2;
        int[] triangles = new int[trinangleCount * 3];

        int idx = 0;
        int ex = trinangleCount / 2;
        for (int i = 0; i < ex; i++)
        {
            triangles[idx++] = 2 * i;
            triangles[idx++] = 2 * i + 1;
            triangles[idx++] = 2 * i + 2;

            triangles[idx++] = 2 * i + 1;
            triangles[idx++] = 2 * i + 2;
            triangles[idx++] = 2 * i + 3;
        }

        if (count % 2 == 1)
        {
            triangles[idx++] = 2 * ex;
            triangles[idx++] = 2 * ex + 1;
            triangles[idx++] = 2 * ex + 2;
        }

        Mesh filledGraphMesh = new Mesh();
        filledGraphMesh.vertices = graphPoints;
        filledGraphMesh.triangles = triangles;

        GameObject filledGraph = new GameObject("Filled graph");
        CanvasRenderer renderer = filledGraph.AddComponent<CanvasRenderer>();
        renderer.SetMesh(filledGraphMesh);
        renderer.SetMaterial(innerFill, null);
        filledGraph.transform.SetParent(InnerFilledGroup);
        filledGraph.transform.localScale = new Vector3(1f, 1f, 1f);
        filledGraph.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}