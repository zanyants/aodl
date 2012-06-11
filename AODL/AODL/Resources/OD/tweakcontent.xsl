<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" 
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:office="urn:oasis:names:tc:opendocument:xmlns:office:1.0" 
                xmlns:style="urn:oasis:names:tc:opendocument:xmlns:style:1.0" 
                xmlns:text="urn:oasis:names:tc:opendocument:xmlns:text:1.0" 
                xmlns:table="urn:oasis:names:tc:opendocument:xmlns:table:1.0" 
                xmlns:draw="urn:oasis:names:tc:opendocument:xmlns:drawing:1.0" 
                xmlns:fo="urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0" 
                xmlns:xlink="http://www.w3.org/1999/xlink" 
                xmlns:dc="http://purl.org/dc/elements/1.1/"
                xmlns:meta="urn:oasis:names:tc:opendocument:xmlns:meta:1.0"
                xmlns:number="urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0"
                xmlns:svg="urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0"
                xmlns:chart="urn:oasis:names:tc:opendocument:xmlns:chart:1.0"
                xmlns:dr3d="urn:oasis:names:tc:opendocument:xmlns:dr3d:1.0" 
                xmlns:math="http://www.w3.org/1998/Math/MathML"
                xmlns:form="urn:oasis:names:tc:opendocument:xmlns:form:1.0"
                xmlns:script="urn:oasis:names:tc:opendocument:xmlns:script:1.0"
                xmlns:ooo="http://openoffice.org/2004/office" 
                xmlns:ooow="http://openoffice.org/2004/writer"
                xmlns:oooc="http://openoffice.org/2004/calc" 
                xmlns:dom="http://www.w3.org/2001/xml-events"
                xmlns:xforms="http://www.w3.org/2002/xforms"
                xmlns:xsd="http://www.w3.org/2001/XMLSchema"
                xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" >
    
  <xsl:output method="xml" indent="yes"/>
  
  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="*[@table:number-columns-repeated]">
    <xsl:call-template name="REPEAT">
      <xsl:with-param name="COUNT" select="@table:number-columns-repeated" />
    </xsl:call-template>  
  </xsl:template>

  <xsl:template match="*[@table:number-rows-repeated]">
    <xsl:call-template name="REPEAT">
      <xsl:with-param name="COUNT" select="@table:number-rows-repeated" />
    </xsl:call-template>
  </xsl:template>

  <xsl:template name="REPEAT">
    <xsl:param name="COUNT"/>
    <xsl:if test="not( $COUNT = 0 )">
      <xsl:copy>
        <xsl:apply-templates select="@* | node()"/>
      </xsl:copy>
      <xsl:call-template name="REPEAT">
        <xsl:with-param name="COUNT" select="$COUNT - 1"/>
      </xsl:call-template>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="@table:number-columns-repeated" />
  <xsl:template match="@table:number-rows-repeated" />

</xsl:stylesheet>
