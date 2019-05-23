# SpaMobileSample


# 動作環境
- Visual Studio 2017 or 2019 または Visual Studio for Mac 2017 or 2019 
- Windows 10 or Mac


# サンプルアプリの概要

アプリ開発で頻出する機能「会員登録」の住所入力部分を、iOS, Android でぞれぞれ SPA と ガワNative, Native アプリで実装し、どのような違いがあるかを体験いただくサンプルです。

簡単な機能ですが、同じ機能を iOS, Android でそれぞれ3種類、合計6種類のソースコードを準備することで、実装方法の違い、動きの違い、使い勝手の違いをご理解いただく一助になれば幸いです。
ただし、今回の目的は「UX/UI の違い」をご理解いただくことに重点を置いており、実装については敢えてライブラリなど使わず素の実装になっており、ベストプラクティスを提示するものではございません。

なお、このサンプルは、マイクロソフト様のイベント de:code 2019 の
[MW52 モバイルアプリ、SPA？ ネイティブ？ UX/UI の違いと技術選択のポイント](https://www.microsoft.com/ja-jp/events/decode/2019session/detail.aspx?sid=MW52) 
で扱うデモアプリと同内容です。ご参加できる方はセッションにもご参加いただけますとよりご理解が深まります。

また、セッションのスライドはイベント終了後公開されますので、公開され次第、こちらにもリンクを張ります。



# このサンプルアプリでできること

* 郵便番号を入力すると住所が表示される。 
* 郵便番号に対するバリデーションが確認できる



# ソリューション内のプロジェクトの説明

## iOS
iOS の郵便番号検索ネイティブアプリのプロジェクト Xamarin.iOS

## JZipSearch.iOSGawa 
iOS の郵便番号検索ガワネイティブアプリのプロジェクト Xamarin.iOS (WKWebView)

## Droid 
Android の郵便番号検索ネイティブアプリのプロジェクト Xamarin.Android

## JZipSearch.DroidGawa 
Android の郵便番号検索ガワネイティブアプリのプロジェクト Xamarin.Android (WebView)

## JZipSearch.Web 
郵便番号検索　SPA のプロジェクト HTML5 + Vue.js





