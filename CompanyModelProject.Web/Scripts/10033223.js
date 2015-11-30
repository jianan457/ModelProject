



















if(typeof doyoo=='undefined' || !doyoo){
var d_genId=function(){
    var id ='',ids='0123456789abcdef';
    for(var i=0;i<34;i++){ id+=ids.charAt(Math.floor(Math.random()*16));  }  return id;
};
var doyoo={
env:{
secure:false,
mon:'/m9100.talk99.cn/monitor',
chat:'/chat7101.talk99.cn/chat',
file:'/static.soperson.com/131221',
compId:10030036,
confId:10033223,
vId:d_genId(),
lang:'',
fixFlash:1,
subComp:0,
_mark:'e7d84f8935bd6d06602ea2aca0175f612279253e52e732bb0126846b2307f63195e3db35b62bd2ef63bf56788702cd9a3d95b08c5c024816'
}

, monParam:{
index:-1,
preferConfig:0,

style:{mbg:'',mh:0,mw:0,
elepos:'0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0',
mbabg:'',
mbdbg:'',
mbpbg:''},

title:'\u5728\u7ebf\u5ba2\u670d',
text:'\u5c0a\u656c\u7684\u5ba2\u6237\u60a8\u597d\uff0c\u6b22\u8fce\u5149\u4e34\u6211\u516c\u53f8\u7f51\u7ad9\uff01\u6211\u662f\u4eca\u5929\u7684\u5728\u7ebf\u503c\u73ed\u5ba2\u670d\uff0c\u70b9\u51fb\u201c\u5f00\u59cb\u4ea4\u8c08\u201d\u5373\u53ef\u4e0e\u6211\u5bf9\u8bdd',
auto:-1,
group:'10037409',
start:'00:00',
end:'24:00',
mask:false,
status:true,
fx:0,
mini:2,
pos:0,
offShow:1,
loop:0,
autoHide:0,
hidePanel:0,
miniStyle:'1',
miniWidth:'340',
miniHeight:'490',
showPhone:0,
monHideStatus:[0,0,0],
monShowOnly:'',
autoDirectChat:-1,
allowMobileDirect:0
}


, panelParam:{
category:'icon',
preferConfig:0,
position:1,
vertical:145,
horizon:5


,mode:1,
target:'10037409',
online:'/statics/plugin/imgs/talk99/zxk.png',
offline:'/statics/plugin/imgs/talk99/zxk.png',
width:133,
height:301,
status:1,
closable:1,
regions:[{type:"2",l:"9",t:"38",w:"119",h:"32",bk:"",v:"10037409"},{type:"2",l:"9",t:"75",w:"119",h:"32",bk:"",v:"10037409"},{type:"2",l:"9",t:"112",w:"119",h:"32",bk:"",v:"10037409"},{type:"0",l:"9",t:"194",w:"119",h:"32",bk:"",v:"2355899855"},{type:"3",l:"13",t:"234",w:"111",h:"32",bk:"",v:"10037409"}],
collapse:0



}



,sniffer:{
ids:'fuzzy99',
gids:'10037466'
}

};

if(typeof talk99Init == 'function'){
    talk99Init(doyoo);
}
if(!document.getElementById('doyoo_panel')){


document.write('<div id="doyoo_panel"></div>');


document.write('<div id="doyoo_monitor"></div>');


document.write('<div id="talk99_message"></div>')

document.write('<div id="doyoo_share" style="display:none;"></div>');
document.write('<lin'+'k rel="stylesheet" type="text/css" href="/static.soperson.com/131221/talk99.css?150728"></li'+'nk>');
document.write('<scr'+'ipt type="text/javascript" src="/static.soperson.com/131221/talk99.js?151105" charset="utf-8"></scr'+'ipt>');

}
}
