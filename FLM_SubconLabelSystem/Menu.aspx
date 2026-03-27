<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Menu.aspx.vb" Inherits="Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <title><%=ConfigurationManager.AppSettings("title")%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=11" />
    <link href="css/ext-all.css" rel="stylesheet" type="text/css" />
    <link href="css/Header.css" rel="stylesheet" type="text/css" />
    <link href="css/Menu.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-3.4.1.js" type="text/javascript"></script>
<%--    <script src="js/jquery-1.4.3.js" type="text/javascript"></script>--%>
    <script src="js/ext-base.js" type="text/javascript"></script>
    <script src="js/ext-all.js" type="text/javascript"></script>
    <script type="text/javascript">    
	    function change_vs(obj) {
	        var vs_id = obj.checked;

	        if (vs_id)
	            Ext.get(obj.id + "-vs").setStyle('display', 'list-item');
	        else
	            Ext.get(obj.id + "-vs").setStyle('display', 'none');
	    }

	    Ext.onReady(function() {

	        Ext.state.Manager.setProvider(new Ext.state.CookieProvider());

	        var viewport = new Ext.Viewport({
	            layout: 'border',
	            items: [
                new Ext.BoxComponent({ // raw
                    region: 'north',
                    el: 'north',
                    height: 100
                }),
				{
				    region: 'west',
				    id: 'west-panel',
				    title: 'Menu',
				    split: true,
				    width: 200,
				    minSize: 175,
				    maxSize: 300,
				    collapsible: true,
				    margins: '0 0 0 5',
				    layout: 'accordion',
				    layoutConfig: {
				        animate: true
				    },
				    items: <%= _list.ToString() %>
				},
                {
                    region: 'center',
                    margins: '0',
                    layout: 'column',
                    autoScroll: true,
                    items: [{
                        columnWidth: 1,
                        title: 'Page',
                        contentEl: 'main-div'
                        }]
                    }
              ]
	        });
	    });
	</script>
    <style type="text/css">
        div#ext-gen9{overflow-y:auto;}
        div.home a{color:#FFFFFF}
        
        .title
        {
           color : #FFFFFF; 
           font-size : 25px;	
           font-family : Arial;
        }
    </style>
    <script type="text/javascript">
		function setIframeHeight(iframeName) {
		  var iframeEl = document.getElementById? document.getElementById(iframeName): document.all? document.all[iframeName]: null;
		  if (iframeEl) {
		  iframeEl.style.height = "auto"; // helps resize (for some) if new doc shorter than previous
		  // need to add to height to be sure it will all show
		  var h = alertSize();
		  var new_h = (h-148);
		  iframeEl.style.height = new_h + "px";
		  }
		}

		function alertSize() {
		  var myHeight = 0;
		  if( typeof( window.innerWidth ) == 'number' ) {
		    //Non-IE
		    myHeight = window.innerHeight;
		  } else if( document.documentElement && ( document.documentElement.clientWidth || document.documentElement.clientHeight ) ) {
		    //IE 6+ in 'standards compliant mode'
		    myHeight = document.documentElement.clientHeight;
		  } else if( document.body && ( document.body.clientWidth || document.body.clientHeight ) ) {
		    //IE 4 compatible
		    myHeight = document.body.clientHeight;
		  }
		  return myHeight;
		}
		
		$(document).ready(function(){
        var pswexpired = '<%=Session("pswexpired")%>';
        if (pswexpired == 1){window.open('MasterMaint/ChangePassword.aspx', '_self');
        var links = document.getElementsByTagName('a');
        for(var i=0, max=links.length; i<max; i++) {
            links[i].setAttribute('disabled', 'disabled');
        }
        
        document.getElementById('sobutton').removeAttribute('disabled');
        
        } 
        });
        
//        var content_start_loading = function() {
//        
//        var pswexpired = '<%=Session("pswexpired")%>';
//        var location = document.getElementById("frContent").contentWindow.location.pathname.split('/').slice(-1);
//        
//        if (pswexpired == 1){
//        window.open('MasterMaint/ChangePassword.aspx', 'page');
//        }

//        };
//        
//           
//        var content_finished_loading = function(iframe) {
//        iframe.contentWindow.onunload = content_start_loading;
//        };
        
	</script>
</head>
<body onload="setIframeHeight('frContent');" onresize="setIframeHeight('frContent');">
  <asp:Literal ID="liItems" runat="server"></asp:Literal>
  <div class="remark" id="main-div">
    <iframe scrolling="auto" name="page" frameborder="0" width="100%" id="frContent"></iframe>
  </div>
  <div id="north">
    <div id="divinfo">
        <table width="100%">
        <tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td>
        <div align="left" class="title"><h1></h1></div></td>
        <td align=right>
        <div>
            <span><%=Me._words%>, <%= Session("gettemp") %> </span>
        </div>
        <br />
        <div class="time">
            <span>Date : <%=Session("LoginHis")%> </span>
        </div>
        <br />
        <div class="home" id="trhome" runat="server">  
            <a id="sobutton" target="_parent" href='<%= Me.SignOutURL() %>'>Log Out</a>           
            <a id="hobutton" target="_parent" href='<%= Me.HomeURL() %>'>Home</a>         
        </div>
        <div class="home" id="resetpwd" runat="server">
            <a class="changepwd" target="page" href='<%= Me.ResetURL() %>'>Please Update Your Password !</a>  
        </div>
        </td></tr></table>
        <div class="clear"></div>
    </div>
    <img class="imgheader" src="image/header2x.jpg" />
  </div>
  
</body>
</html>

