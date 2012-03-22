
// http://www.w3schools.com/html5/
// http://www.w3schools.com/html5/tryit.asp?filename=tryhtml5_video_bear

namespace Html5Tutorial

open IntelliFactory.WebSharper

module BearJs =

    open IntelliFactory.WebSharper.Html

    [<JavaScript>]
    let Video x = Default.HTML5.Tags.Video x
    [<JavaScript>]
    let Source x = Default.HTML5.Tags.Source x
    [<JavaScript>]
    let Type x = Default.Attr.Type x
    [<JavaScript>]
    let Controls x = Attr.NewAttr "controls" x
    [<JavaScript>]
    let Autoplay x = Attr.NewAttr "autoplay" x

    [<JavaScript>]
    let video() : IPagelet = 
        Video [Width "320"; Height "240"; Controls "controls"; Autoplay "autoplay"]
        -<
        [
        Source [Src "http://www.w3schools.com/html5/movie.mp4"; Type "video/mp4"]
        Source [Src "http://www.w3schools.com/html5/movie.ogg"; Type "video/ogg"]
        Source [Src "http://www.w3schools.com/html5/movie.webm"; Type "video/webm"]
        ]
        -< [ Text "Your browser does not support the video tag." ]
        :> IPagelet

type VideoControl() =
    inherit Web.Control()
    [<JavaScript>]
    override this.Body = BearJs.video()

type Action = 
    | VideoBearHtml
    | VideoBearJs

module BearHtml =

    open IntelliFactory.WebSharper.Sitelets
    open IntelliFactory.Html
    open IntelliFactory.Html.Tags
    open IntelliFactory.Html.Tags.HTML5
    open IntelliFactory.Html.Attributes
    
    // video element attributes
    let Controls x = Html.NewAttribute "controls" x
    let Autoplay x = Html.NewAttribute "autoplay" x
    
    let pageContent body : Content<Action> =
        PageContent <| fun context ->
            { Page.Default with
                Doctype = Some "<!DOCTYPE html>" // HTML5
                Body = body context
            }

    let videoBearHtml =
        pageContent <| fun ctx ->
            [Video
                [Width "320"; Height "240"; Controls "controls"; Autoplay "autoplay"]
                -< 
                [
                Source [Src "http://www.w3schools.com/html5/movie.mp4"; Type "video/mp4"]
                Source [Src "http://www.w3schools.com/html5/movie.ogg"; Type "video/ogg"]
                Source [Src "http://www.w3schools.com/html5/movie.webm"; Type "video/webm"]
                ]
                -< [ Text "Your browser does not support the video tag." ]
            ]

    let videoBearJs =
        pageContent <| fun ctx ->
            [Div [new VideoControl()]]

open IntelliFactory.WebSharper.Sitelets
open BearHtml

type Website() =
    interface IWebsite<Action> with
        
        member this.Actions =
            [
                VideoBearHtml
                VideoBearJs
            ]
        
        member this.Sitelet =
            [
                Sitelet.Content "/video_bear_html" VideoBearHtml videoBearHtml
                Sitelet.Content "/video_bear_js" VideoBearJs videoBearJs
            ]
            |> Sitelet.Sum

[<assembly: WebsiteAttribute(typeof<Website>)>]
do ()
