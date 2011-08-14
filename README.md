Posterbuilder is a little [C#] class library for creating dynamic images (e.g. posters) based on a given template.  

As an example I have a little website [toepoke.co.uk](http://toepoke.co.uk) for organising 5-a-side football matches.  I use the poster builder to allow the user to download a poster which they can put up at work asking people in they'd like to play as well.

Where Posterbuilder comes in is we take a template image with the site branding, logo, etc and add text and images that are appropriate to the end-user account, giving a personalised poster.  

<table border="0" width="60%">
<tr><td align="middle">
  <a href="http://dl.dropbox.com/u/1055915/blog/poster-builder/thumbnails/large/map-example-template.jpg">
    <img src="http://dl.dropbox.com/u/1055915/blog/poster-builder/thumbnails/small/map-example-template.jpg"
      title="template image" />
  </a>
</td>
<td>
   <a href="http://dl.dropbox.com/u/1055915/blog/poster-builder/thumbnails/large/map-example-rendered.png">
     <img src="http://dl.dropbox.com/u/1055915/blog/poster-builder/thumbnails/small/map-example-rendered.png"
      title="personalised poster" />
   </a>
</td>
</tr>

<tr>
<td>Template image.</td>
<td>Personalised image.</td>
</tr>
</table>

In this instance we have added:

+ Some text saying telling the reader where and how often the game takes place.
+ A unique URL to the sign-up page of the game
+ A [quick-response code](http://en.wikipedia.org/wiki/QR_code) which also navigates to the sign-up page of the game (useful for people with compatible mobile phones)
+ A map showing where the game is played.

The above example and two further examples are included with the source code.  Sadly I can't provide an on-line demo at this time.

Feel free to dig straight into the [source code](https://github.com/toepoke/PosterBuilder) or look through the accompanying documentation.

- [[Example walkthrough|Poster-Creation-Walkthrough:-Part-1---Design-Rules]]
- [[Code overview|Code-overview]]




