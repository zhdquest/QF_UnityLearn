using UnityEngine;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Collections;

public class SQLFramework {

    #region 单例 Singleton

    private static SQLFramework instance;

    public static SQLFramework GetInstance()
    {
        if (instance == null)
        {
            instance = new SQLFramework();
        }
        return instance;
    }

    protected SQLFramework()
    {
    }

    #endregion

    #region SQL Fields

    private SqliteConnection con;
    private SqliteCommand command;
    private SqliteDataReader reader;
    
    #endregion
    
    #region SQL Frame
    
    /// <summary>
    /// 打开数据库
    /// </summary>
    /// <param name="dbName"></param>
    public void OpenDatabase(string dbName)
    {
        //动态添加后缀
        if (!dbName.EndsWith(".sqlite"))
        {
            dbName += ".sqlite";
        }

        //数据库路径
        string dbPath = "";

        //预编译指令：表示如果在Unity中运行或在PC端运行应用程序，就执行下列代码
#if UNITY_EDITOR || UNITY_STANDALONE
        dbPath = "Data Source = "
                 + Application.streamingAssetsPath
                 + "/" + dbName;
#endif
        //实例化连接对象
        con = new SqliteConnection(dbPath);
        //打开连接
        con.Open();
        //实例化指令对象
        command = con.CreateCommand();
    }

    /// <summary>
    /// 关闭数据库
    /// </summary>
    public void CloseDatabase()
    {
        con.Close();
        command.Dispose();

        con = null;
        command = null;
    }


    /// <summary>
    /// 仅仅执行SQL语句
    /// </summary>
    public int JustExecute(string sqlQuery)
    {
        //设置SQL语句
        command.CommandText = sqlQuery;
        //执行SQL语句
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// 查询单个数据
    /// </summary>
    /// <param name="sqlQuery"></param>
    /// <returns></returns>
    public object SelectSingleData(string sqlQuery)
    {
        //设置SQL语句
        command.CommandText = sqlQuery;
        //执行SQL语句
        return command.ExecuteScalar();
    }

    /// <summary>
    /// 查询多个数据
    /// </summary>
    /// <param name="sqlQuery"></param>
    public List<ArrayList> SelectMultipleData(string sqlQuery)
    {
        //设置SQL语句
        command.CommandText = sqlQuery;
        //执行SQL语句
        reader = command.ExecuteReader();
        //创建List<ArrayList>
        List<ArrayList> result = new List<ArrayList>();

        while (reader.Read())
        {
            //新建ArrayList存储当行的数据
            ArrayList rowData = new ArrayList();
            //遍历所有的列
            for (int i = 0; i < reader.FieldCount; i++)
            {
                //将当前行的当前列添加到rowData
                rowData.Add(reader.GetValue(i));
            }
            //将储存好的当前行数据添加到List
            result.Add(rowData);
        }
        //关闭读取器
        reader.Close();
        //返回结果
        return result;
    }

    #endregion
}