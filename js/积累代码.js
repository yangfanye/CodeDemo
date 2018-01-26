//继承并复写js的方法;在后期添加函数时采用这种方式;这样的话,可以防止变量污染
Function.prototype.addMethod = function (name, fn) {
    this[name] = fn;
    //方便使用链式方法
    return this;
}
//Demo
var methods = function(){};
methods.addMethod('checkName',function(){
    //逻辑
    console.log(0);
    return this;
}).addMethod('checkEmail',function(){
    //逻辑
    console.log(1);
    return this;
});
//随机数
methods.addMethod('RandomNum', function () {
    var num = "";
    for (var i = 0; i < 8; i++) {
        num += Math.floor(Math.random() * 10);
    }
    return num;
})
console.log(methods.RandomNum());
//methods.checkName().checkEmail();

//安全模式创建的工厂类
var Factory = function(type,content){
    //instanceof:实例this在不在Factory函数中
    if(this instanceof Factory){
        var s = new this[type](content);
        return s;
    }else{
        return new Factory(type,content);
    }
}

console.log((new Date()).valueOf());