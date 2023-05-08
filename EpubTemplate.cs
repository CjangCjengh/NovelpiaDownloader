namespace NovelpiaDownloader
{
    public static class EpubTemplate
    {
        public static string container = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
            "<container version=\"1.0\" xmlns=\"urn:oasis:names:tc:opendocument:xmlns:container\">\n" +
            "<rootfiles>\n" +
            "<rootfile full-path=\"OEBPS/content.opf\" media-type=\"application/oebps-package+xml\"/>\n" +
            "</rootfiles>\n" +
            "</container>\n";
        public static string sgctoc = "div.sgc-toc-title {\n" +
            "font-size: 2em;\n" +
            "font-weight: bold;\n" +
            "margin-bottom: 1em;\n" +
            "text-align: center;\n" +
            "text-indent: 1.0em;\n" +
            "margin-top: 1.0em;\n" +
            "}\n" +
            "\n" +
            "div.sgc-toc-level-1 {\n" +
            "margin-left: 0em;\n" +
            "text-indent: 1.0em;\n" +
            "margin-top: 0.2em;\n" +
            "line-height: 1.6em;\n" +
            "}\n" +
            "\n" +
            "div.sgc-toc-level-2 {\n" +
            "margin-left: 2em;\n" +
            "}\n" +
            "\n" +
            "div.sgc-toc-level-3 {\n" +
            "margin-left: 2em;\n" +
            "}\n" +
            "\n" +
            "div.sgc-toc-level-4 {\n" +
            "margin-left: 2em;\n" +
            "}\n" +
            "\n" +
            "div.sgc-toc-level-5 {\n" +
            "margin-left: 2em;\n" +
            "}\n" +
            "\n" +
            "div.sgc-toc-level-6 {\n" +
            "margin-left: 2em;\n" +
            "}\n";
        public static string stylesheet = ".border01 {\n" +
            "\n" +
            "border: 2px solid black;\n" +
            "padding: 1.0em;\n" +
            "margin: 1.0em;\n" +
            "line-height: 1.6em;\n" +
            "font-size: 1.0em;\n" +
            "font-style: normal;\n" +
            "font-weight: normal;\n" +
            "text-align: justify;\n" +
            "}\n" +
            "\n" +
            "\n" +
            "/* 바디 기본 설정 */\n" +
            "/* 나눔고딕체 설정 필요 (임베딩 불가)*/\n" +
            "\n" +
            "body{\n" +
            "margin:0;\n" +
            "padding:0;\n" +
            "font-family: 'KoPub바탕체 Light', 'KoPub돋움체 Light';\n" +
            "}\n" +
            "\n" +
            "/*폰트 고딕*/\n" +
            "\n" +
            ".dotum{font-family:'KoPub돋움체 Light';}\n" +
            "\n" +
            "\n" +
            "/* 목차 설정까지 한큐 */\n" +
            "\n" +
            "h1{\n" +
            "display: block;\n" +
            "font-size: 1.2em;\n" +
            "font-style: normal;\n" +
            "font-weight: bold;\n" +
            "line-height: 1.6em;\n" +
            "margin-bottom: 0;\n" +
            "margin-left: 0;\n" +
            "margin-right: 0;\n" +
            "margin-top: 1.6em;\n" +
            "text-align: center;\n" +
            "text-indent: 0;\n" +
            "padding-left: 0;\n" +
            "padding-right: 0;\n" +
            "padding-top: 0;\n" +
            "}\n" +
            "\n" +
            "/* 커버 */\n" +
            "\n" +
            "div{\n" +
            "font-style: normal;\n" +
            "font-weight: normal;\n" +
            "}\n" +
            "\n" +
            "\n" +
            "/* 본문 */\n" +
            "\n" +
            "p{\n" +
            "font-size: 1.0em;\n" +
            "font-style: normal;\n" +
            "font-weight: normal;\n" +
            "line-height: 1.6em;\n" +
            "margin-bottom: 0;\n" +
            "margin-left: 0;\n" +
            "margin-right: 0;\n" +
            "margin-top: 0.2em;\n" +
            "text-align: justify;\n" +
            "text-indent: 1.0em;\n" +
            "padding-left: 0;\n" +
            "padding-right: 0;\n" +
            "}\n" +
            "\n" +
            "/*크게, 작게*/\n" +
            "\n" +
            ".t09{\n" +
            "font-size: 0.9em;\n" +
            "font-style: normal;\n" +
            "font-weight: normal;\n" +
            "line-height: 1.6em;\n" +
            "margin-bottom: 0em;\n" +
            "margin-left: 0;\n" +
            "margin-right: 0;\n" +
            "margin-top: 0.2em;\n" +
            "text-indent: 0em;\n" +
            "padding-left: 0;\n" +
            "padding-right: 0;\n" +
            "}\n" +
            "\n" +
            ".t12{\n" +
            "font-family: 'ng',serif;\n" +
            "font-size: 1.2em;\n" +
            "font-style: normal;\n" +
            "font-weight: bold;\n" +
            "line-height: 1.6em;\n" +
            "margin-bottom: 0em;\n" +
            "margin-left: 0;\n" +
            "margin-right: 0;\n" +
            "margin-top: 0.2em;\n" +
            "text-align: justify;\n" +
            "text-indent: 1em;\n" +
            "padding-left: 0;\n" +
            "padding-right: 0;\n" +
            "}\n" +
            "\n" +
            "/*중간, 오른쪽, 볼드, 이탤릭, 들여쓰기*/\n" +
            "\n" +
            ".ridicenter{\n" +
            "display: block;\n" +
            "text-align : center;\n" +
            "}\n" +
            ".c{\n" +
            "text-align:center;\n" +
            "}\n" +
            "\n" +
            ".r{\n" +
            "text-align: right;\n" +
            "}\n" +
            "\n" +
            ".b{\n" +
            "font-weight: bold;\n" +
            "}\n" +
            "\n" +
            ".i{\n" +
            "font-style: italic;\n" +
            "}\n" +
            "\n" +
            ".ridibox {\n" +
            "padding: 0.5em 1em ;\n" +
            "}\n" +
            "\n" +
            "/*흐리게*/\n" +
            "\n" +
            ".flashback {\n" +
            "color: #868a8e !important;\n" +
            "}\n" +
            "\n" +
            ".blur{\n" +
            "color: #8F908A;\n" +
            "}\n" +
            "\n" +
            "/*커버, 이미지*/\n" +
            "\n" +
            ".cover {\n" +
            "text-align: center;\n" +
            "width: 100%;\n" +
            "margin-top: 0;\n" +
            "margin-right: 0;\n" +
            "}\n" +
            "\n" +
            ".img{\n" +
            "width:100%;\n" +
            "text-align:center;\n" +
            "margin-top: 0;\n" +
            "margin-right: 0;\n" +
            "}\n" +
            "\n" +
            "\n" +
            "/* * * * */\n" +
            "\n" +
            ".devide{\n" +
            "color:#33394c;\n" +
            "display:block;\n" +
            "font-size:1.0em;\n" +
            "font-style:normal;\n" +
            "font-weight:normal;\n" +
            "line-height:1.5em;\n" +
            "margin-bottom:1.5em;\n" +
            "margin-left:0;\n" +
            "margin-right:0;\n" +
            "margin-top:1.5em;\n" +
            "padding-left:0;\n" +
            "padding-right:0;\n" +
            "padding-top:0;\n" +
            "text-align:center;\n" +
            "text-indent:0;\n" +
            "}\n" +
            "\n" +
            "/*루비문자*/\n" +
            "rt {\n" +
            "font-size: 0.85em;\n" +
            "font-style: normal;\n" +
            "font-weight: normal;\n" +
            "}\n" +
            "\n" +
            "\n" +
            "/*양 옆 들여쓰기*/\n" +
            "\n" +
            ".pad07 {\n" +
            "padding:0 0.7em;\n" +
            "}\n" +
            "/*이미지에 패딩 주고 싶을 때*/\n" +
            "\n" +
            "div#wrap{margin:5pt;}\n" +
            "div#wrap2{ margin: 5pt; text-align: center; }\n" +
            "\n" +
            "/*각주처리*/\n" +
            "\n" +
            "\n" +
            ".footnote p\n" +
            "{\n" +
            "text-indent:0;\n" +
            "font-size:0.85em;\n" +
            "line-height:1.6em;\n" +
            "}\n" +
            "\n" +
            "span.br {\n" +
            "color:gray;\n" +
            "font-size: 0.9em;\n" +
            "}\n" +
            "\n" +
            "/*글상자*/\n" +
            "\n" +
            ".ridiborder {\n" +
            "padding: 0.5em 1em;\n" +
            "border-radius: 0.5em;\n" +
            "border: 0.1em solid #868a8e;\n" +
            "}\n" +
            "\n" +
            "\n" +
            "\n" +
            "\n" +
            ".ridipost {\n" +
            "padding: 0.5em 1em;\n" +
            "background-color: #FFFF00;\n" +
            "border-radius: 0.5em;\n" +
            "}\n";
        public static string toc = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
            "<!DOCTYPE ncx PUBLIC \"-//NISO//DTD ncx 2005-1//EN\"\n" +
            "\"http://www.daisy.org/z3986/2005/ncx-2005-1.dtd\">\n" +
            "<ncx xmlns=\"http://www.daisy.org/z3986/2005/ncx/\" version=\"2005-1\">\n" +
            "<head>\n" +
            "<meta name=\"dtb:depth\" content=\"0\" />\n" +
            "<meta name=\"dtb:totalPageCount\" content=\"0\" />\n" +
            "<meta name=\"dtb:maxPageNumber\" content=\"0\" />\n" +
            "</head>\n" +
            "<docTitle>\n";
        public static string content1 = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
            "<package version=\"2.0\" unique-identifier=\"BookId\" xmlns=\"http://www.idpf.org/2007/opf\">\n" +
            "<metadata xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:opf=\"http://www.idpf.org/2007/opf\">\n";
        public static string content2 = "<meta content=\"1.9.10\" name=\"Sigil version\"/>\n" +
            "</metadata>\n" +
            "<manifest>\n" +
            "<item id=\"ncx\" href=\"toc.ncx\" media-type=\"application/x-dtbncx+xml\"/>\n" +
            "<item id=\"sgc-toc.css\" href=\"Styles/sgc-toc.css\" media-type=\"text/css\"/>\n" +
            "<item id=\"Stylesheet.css\" href=\"Styles/Stylesheet.css\" media-type=\"text/css\"/>\n" +
            "<item id=\"cover.html\" href=\"Text/cover.html\" media-type=\"application/xhtml+xml\"/>\n" +
            "<item id=\"cover.jpg\" href=\"Images/cover.jpg\" media-type=\"image/jpeg\"/>";
        public static string chapter = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
            "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\"\n" +
            "\"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">\n" +
            "\n" +
            "<html xmlns=\"http://www.w3.org/1999/xhtml\">\n" +
            "<head>\n" +
            "<title></title>\n" +
            "<style type=\"text/css\">\n" +
            "html, body { margin:0; padding:0; }\n" +
            "</style>\n" +
            "<link href=\"../Styles/sgc-toc.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" +
            "<link href=\"../Styles/Stylesheet.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" +
            "</head>\n" +
            "<body>\n";
        public static string cover = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
            "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\"\n" +
            "\"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">\n" +
            "\n" +
            "<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:xml=\"http://www.w3.org/XML/1998/namespace\" xml:lang=\"ko\">\n" +
            "<head>\n" +
            "<title></title>\n" +
            "<style type=\"text/css\">\n" +
            "html, body { margin:0; padding:0; }\n" +
            "</style>\n" +
            "\n" +
            "<link href=\"../Styles/sgc-toc.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" +
            "<link href=\"../Styles/Stylesheet.css\" type=\"text/css\" rel=\"stylesheet\"/>\n" +
            "</head>\n" +
            "\n" +
            "<body>\n" +
            "<div class=\"cover\"><img alt=\"cover\" src=\"../Images/cover.jpg\" width=\"100%\"/></div>\n" +
            "</body>\n" +
            "</html>\n";
    }
}
