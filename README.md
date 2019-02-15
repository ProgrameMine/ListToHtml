# ListToHtml
将泛型集合List转换成Html
Nuget： Install-Package ListToTableHtml

使用方式：
引用命名空间：using ListToTableHtml;
Model中需使用标签：Message

ModelUser.cs:

public class ModelUser
{ 

    [Message("用户名称")]
    public string UserName { get; set; }

    [Message("年龄", "岁")]
    public int Age { get; set; }
    
}

Program.cs:
{
static void Main(string[] args)
{

    List<ModelUser> userLists = new List<ModelUser>
    {
         new ModelUser{  UserName="张小凡", Age=420},
         new ModelUser{ UserName="碧瑶",Age=18}
    };

    var table = MessageHelper<ModelUser>.CreateTable("个人信息", userLists, true);

    Console.WriteLine(table);
    Console.ReadKey(); 
}

