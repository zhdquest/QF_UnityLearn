using UnityEngine;

#region Null-LKP

public class Student
{
    public string name;
    public int age;
    public string hobby;
}

public class Grade
{
    public Student[] students;
}

public class School
{
    public Grade[] grades;

    public void ShowMe()
    {
        for (int i = 0; i < grades.Length; i++)
        {
            for (int j = 0; j < grades[i].students.Length; j++)
            {
                Debug.Log("我叫" + grades[i].students[j].name
                    + "今年：" + grades[i].students[j].age + "岁");
            }
        }
    }
}

#endregion

#region LKP

public class NewStudent
{
    private string name;
    private int age;
    private string hobby;
    private string address;

    public void ShowMe()
    {
        Debug.Log("我叫" + name
                       + ",今年：" + age + "岁"
                       + ",爱好：" + hobby
                       + ",家住：" + address);
    }
}

public class NewGrade
{
    private string _gradeNumber;

    private NewStudent[] _students;
    
    public void ShowMe()
    {
        Debug.Log("下面进行第" + _gradeNumber + "年级的自我介绍...");

        for (int i = 0; i < _students.Length; i++)
        {
            _students[i].ShowMe();
        }
    }
}

public class NewSchool
{
    private string schoolName;

    private NewGrade[] _grades;

    public void ShowMe()
    {
        Debug.Log("下面是" + schoolName + "的自我介绍...");

        for (int i = 0; i < _grades.Length; i++)
        {
            _grades[i].ShowMe();
        }
    }
}

#endregion

public class LKPDemo : MonoBehaviour {
    
}