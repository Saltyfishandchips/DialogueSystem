VAR choice = 0

    {
        -choice == 1:  
            -> fanyang  
        -choice == 2:  
            -> guiying 
        -choice == 3:
            -> yijiao
        -choice == 4:
            -> nohandle
    }  
  
== fanyang
    // 返阳对话写在下方
    鬼魂：好像有什么地方不对劲...
    金翎（秦广王）：此人阳寿已尽，怎么给人家放走了？快抓回来！
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：下辈子我还要环球旅行！
    林象辰：祝你投个好胎。
    ->END

== yijiao
    // 移交对话写在下 第一天无移交按钮
    移交
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END

