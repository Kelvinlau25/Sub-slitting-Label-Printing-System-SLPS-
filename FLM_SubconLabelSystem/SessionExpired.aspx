<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionExpired.aspx.cs" Inherits="SessionExpired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Session Time Out</title>
</head>
<body>
    <h2>Session Expired</h2>
    <p>
        <span style="color:White;">Your session has expired due to inactivity.</span>
        <br />
        <br />
        <a target="_parent" href="<%= ResolveUrl(ReturnURL) %>">Click here to login again</a>
    </p>
</body>
</html>