using UnityEngine;
using Mono.Data.Sqlite;

public class SQLDemo : MonoBehaviour
{
    //数据库连接对象
    private SqliteConnection con;
    //数据库指令对象
    private SqliteCommand command;
    //数据库读取对象
    private SqliteDataReader reader;

    private string connectionStr;
    
    private void Start()
    {
        // Debug.Log(Application.streamingAssetsPath);
        //数据源路径
#if UNITY_EDITOR
        connectionStr = "Data Source = " + 
                        Application.streamingAssetsPath +
                        "/HeroDatabase.sqlite";
#endif

#if UNITY_ANDROID
        //在Android平台的路径设置
#endif

#if UNITY_IOS
        //在iOS平台的路径设置
#endif
       
        //实例化连接对象
        con = new SqliteConnection(connectionStr);
        //打开连接
        con.Open();
        //创建指令对象
        command = con.CreateCommand();
        
        //执行操作
        // SQLInsert();
        // SQLUpdate();
        // SQLSelectSingleData();
        SQLSelectMutipleData();
    }

    /// <summary>
    /// 查多个数据
    /// </summary>
    private void SQLSelectMutipleData()
    {
        //设置SQL语句
        command.CommandText = "Select * From HeroTable";
        //执行SQL语句，并返回所有查询到的结果到读取器
        reader = command.ExecuteReader();
        
        //如果读取数据？
        
        //读取下一行数据，如果没有下一行返回false，否则返回true
        while (reader.Read())
        {
            Debug.Log(reader.FieldCount);//列数

            for (int i = 0; i < reader.FieldCount; i++)
            {
                //读到了当前行的第i列的值
                object val = reader.GetValue(i);
                
                Debug.Log(val);
            }
        }
        
        //关闭读取器
        reader.Close();
    }
    
    private void SQLSelectSingleData()
    {
        //设置SQL语句
        command.CommandText = "Select HeroAD From HeroTable Where HeroName='皮城女警'";
        //执行SQL语句，返回查询到的第一个结果【适用于查询一个结果（一行一列）】
        object selectResult = command.ExecuteScalar();
        //打印结果
        Debug.Log(selectResult);
    }

    private void SQLUpdate()
    {
        //设置SQL语句
        command.CommandText = "Update HeroTable Set HeroLevel=2 WHERE HeroName='皮城女警'";
        //执行SQL语句，并返回受影响的行数【适用于增删改】
        int rows = command.ExecuteNonQuery();
    }

    private void SQLInsert()
    {
        //设置SQL语句
        command.CommandText = "Insert Into HeroTable VALUES ('皮城女警',1,120,20)";
        //执行SQL语句，并返回受影响的行数【适用于增删改】
        int rows = command.ExecuteNonQuery();
    }

    /// <summary>
    /// 当应用程序关闭时调用一次
    /// </summary>
    private void OnApplicationQuit()
    {
        //释放对象
        command.Dispose();
        //关闭连接
        con.Close();
    }
}