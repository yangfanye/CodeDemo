//异步读取图片地址
  function getMemberfaceid(membersList){
    var count =0;//自定义一个函数
    
    function getMember(member){
      console.log(member);
      $.get("/members/memberfaces/" +member.memberid,function(result){
        $("#"+member.memberid).attr("src",'/members/face/'+result.data.faces[0]);
        count++;//加一
        //重点代码
        if(count<membersList.length){
          getMember(membersList[count]);
        }
      });
    }
    getMember(membersList[count]);   
  }