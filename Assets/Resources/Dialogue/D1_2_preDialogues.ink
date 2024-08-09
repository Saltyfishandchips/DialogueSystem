// VAR choice = 0
// ->d1_2
// == d1_2
// 林象辰：师傅你是做什么工作的？#Layout:Right
// 鬼魂：……（凝视）#Layout:Left
// 林象辰：<color=red>师傅</color>你叫什么名字啊？#Layout:Right
// 鬼魂：……#Layout:Left
// ->sec2

// == sec2
// * 告知李捷已死 #Layout:Right
//     ->sec2
// * [不告知李捷已死]
//     ->sec3
// * [告知李捷已死，且短暂返阳找她]
//     ->sec3

// == sec3
// 你觉得呢#Layout:Left
//     -> END

VAR choice = 0

    {
        -choice == 1:  
            -> choice_1  
        -choice == 2:  
            -> choice_2 
        -choice == 3:
            -> choice_3
        -choice == 4:
            -> choice_4
        -choice == 5:
            -> choice_5
        -choice == 6:
            -> choice_6
        -choice == 7:
            -> choice_7
        -choice == 8:
            -> choice_8
        -choice == 9:
            -> choice_9
        -choice == 10:
            -> choice_10
    }  
  
== choice_1
    // 是否提交路引对话写在下方
    是否提交路引 #Layout:Left
    ->END
  
== choice_2
    // 姓名对话写在下方
    姓名是否一致 #Layout:Left
    ->END

== choice_3
    // 性别对话写在下
    性别是否一致#Layout:Left
    ->END

== choice_4
    // 生辰八字对话写在下
    生辰八字是否一致#Layout:Left
    ->END
    
== choice_5
    // 鬼差对话写在下
    抓捕鬼差是否一致#Layout:Left
    ->END

== choice_6
    // 盖章栏对话写在下
    盖章栏#Layout:Left
    ->END
    
== choice_7
    // 不处理对话写在下
    不处理#Layout:Left
    ->END
    
== choice_8
    // 不处理对话写在下
    不处理#Layout:Left
    ->END
    
== choice_9
    // 不处理对话写在下
    不处理#Layout:Left
    ->END

== choice_10
    // 不处理对话写在下
    不处理#Layout:Left
    ->END
    
->END

