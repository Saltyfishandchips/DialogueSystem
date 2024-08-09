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
    鬼魂：回家吃饭喽！
    金翎（秦广王）：你的服务意识需要加强。
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：饭还热乎着。
    ->END

== yijiao
    // 移交对话写在下
    鬼魂：赶紧走，一会儿外卖就凉了。
    金翎（秦广王）：你的服务意识需要加强。
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END