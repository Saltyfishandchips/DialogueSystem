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
    鬼魂：记得刚刚的手续在email里CC我一下。
    ->END
  
== guiying
    // 归阴对话写在下方
    鬼魂：你要想想这件事是否make sense？
    金翎（秦广王）：姓名有误，理应返阳！
    ->END

== yijiao
    // 移交对话写在下
    鬼魂：这个point之前已经和鬼差align过了，现在有什么问题吗？
    金翎（秦广王）：姓名有误，理应返阳！
    ->END

== nohandle
    // 不处理对话写在下
    不处理
    
->END