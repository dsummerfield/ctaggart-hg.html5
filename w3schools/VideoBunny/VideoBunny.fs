
// http://www.w3schools.com/html5/html5_video_dom.asp

namespace Html5Tutorial

open IntelliFactory.WebSharper

module BunnyJs =

    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.Html5

    [<JavaScript>]
    let Video x = HTML5.Tags.Video x
    [<JavaScript>]
    let Source x = HTML5.Tags.Source x
    [<JavaScript>]
    let Type x = Attr.Type x

    [<JavaScript>]
    let videoJs() : IPagelet = 
        
        // HTML DOM Layout (via JavaScript)

        let playPause = Button [Text "Play/Pause"]
        let makeBig = Button [Text "Big"]
        let makeSmall = Button [Text "Small"]
        let makeNormal = Button [Text "Normal"]

        let id = "video1"

        let videoEl =
            Video [Id id; Width "420"]
            -<
            [
            Source [Src "http://www.w3schools.com/html5/mov_bbb.mp4"; Type "video/mp4"]
            Source [Src "http://www.w3schools.com/html5/mov_bbb.ogg"; Type "video/ogg"]
            ]
            -< [Text "Your browser does not support HTML5 video."]
        
        let div = Div [Attr.Style "text-align:center"] -< [playPause; makeBig; makeSmall; makeNormal; Br[]; videoEl]

        // JavaScript Behavior

        let video() = As<HTMLVideoElement>(ById id)

        playPause |> OnClick (fun _ _ ->
            let v = video()
            if v.Paused then v.Play() else v.Pause()
        )

        makeBig |> OnClick (fun _ _ -> video().Width <- "560")
        makeSmall |> OnClick (fun _ _ -> video().Width <- "320")
        makeNormal |> OnClick (fun _ _ -> video().Width <- "420")

        div :> IPagelet

type VideoControl() =
    inherit Web.Control()
    [<JavaScript>]
    override this.Body = BunnyJs.videoJs()

type Action = 
    | VideoBunny

module BunnyHtml =

    open IntelliFactory.WebSharper.Sitelets
    open IntelliFactory.Html

    let pageContent body : Content<Action> =
        PageContent <| fun context ->
            { Page.Default with
                Doctype = Some "<!DOCTYPE html>" // HTML5
                Body = body context
            }

    let videoBunny =
        pageContent <| fun ctx ->
            let vc = Div [new VideoControl()]
            let link = A [HRef "http://www.bigbuckbunny.org/"] -< [Text "Big Buck Bunny"]
            [Div [vc; P [Text "Video courtesy of "; link; Text "."]]]

open IntelliFactory.WebSharper.Sitelets
open BunnyHtml

type Website() =
    interface IWebsite<Action> with
        
        member this.Actions =
            [
                VideoBunny
            ]
        
        member this.Sitelet =
            [
                Sitelet.Content "/VideoBunny" VideoBunny videoBunny
            ]
            |> Sitelet.Sum

[<assembly: WebsiteAttribute(typeof<Website>)>]
do ()
