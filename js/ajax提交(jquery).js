//活动报名表提交
        function submitBarSignUp() {
            var token = $.trim($("#hidToken").val());
            var BossID = $("#hidBossID").val();
            var BarID = $("#hidBarID").val();
            var ReOpenID = $("#hidOpenID").val();
            var NickName = $("#hidNickName").val();
            var Sex = $("#hidSex").val();
            var Province = $("#hidProvince").val();
            var City = $("#hidCity").val();
            var Country = $("#hidCountry").val();
            var HeadImgUrl = $("#hidHeadImgUrl").val();
            var EndPrice = $("#hidEndPrice").val();

            var collectInfo = Array();
            for (var i = 0; i < 6; i++) {
                var value = "";
                value = $("#members").find("input").eq(i).val();
                collectInfo[i] = value ? value : "";
            }
            if ($("#CollectInfo1").val().length == 0) {
                WxDialog("姓名不能为空", 200);
                return false;
            }
            if (collectInfo[1].length != 11 || isNaN(collectInfo[1])) {
                WxDialog("请输入正确的手机号", 200);
                return false;
            }

            $.ajax({
                type: "POST",
                url: "../Marketing/BargainingSignUp_Submit".getJumpUrl(),
                data: { BossID: BossID, BarID: BarID, ReOpenID: ReOpenID, NickName: NickName, Sex: Sex, Province: Province, City: City, Country: Country, HeadImgUrl: HeadImgUrl },
                dataType: "json",
                success: function (result) {
                    if (result.messageCode == 5) {
                        WxDialog("报名成功.", 200);
                        var idUrl = "/Marketing/BargainingSignUp?id=" + result.messageId + "&token=" + token + "&barid=" + BarID;
                        $("#hidlocation").val(idUrl);
                    }
                    else if (result.messageCode == -6) {
                        WxDialog("您已参加过这个活动,马上为您跳转.", 200);
                        var idUrl = "/Marketing/BargainingSignUp?id=" + result.messageId + "&token=" + token + "&barid=" + BarID;
                        $("#hidlocation").val(idUrl);
                        //parent.location.href = "/Marketing/BargainingSignUp?id=" + result.messageId + "&token=" + token + "&barid=" + BarID ;
                    }
                    else {
                        WxDialog("网络不给力，请重试！", 200);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    pageAlert(errorThrown, 2000);
                }
            });
        }
        //活动报名表提交