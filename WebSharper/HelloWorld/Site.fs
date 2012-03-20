namespace Samples

open IntelliFactory.WebSharper.Sitelets

module SampleSite =
    open IntelliFactory.Html

    type Action = 
        | HelloWorld

    let helloWorld =
        PageContent <| fun context ->
            {
                Page.Default with
                    Doctype = Some "<!DOCTYPE html>" // HTML5
                    Title = Some "Sample - HelloWorld"
                    Body = [ Div [ new HelloWorldViewer() ] ]
            }  

open SampleSite

type SampleWebsite() =
    interface IWebsite<Action> with
        
        member this.Actions =
            [
                HelloWorld
            ]
        
        member this.Sitelet =
            [
                Sitelet.Content "/HelloWorld" HelloWorld helloWorld
            ]
            |> Sitelet.Sum

[<assembly: WebsiteAttribute(typeof<SampleWebsite>)>]
do ()
