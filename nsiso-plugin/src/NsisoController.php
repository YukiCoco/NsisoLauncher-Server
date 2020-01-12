<?php

namespace Nsiso;

use Illuminate\Http\Request;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\DB;
use Nsiso\Models\Ad;

class NsisoController extends Controller
{
    public function addAd(Request $request){
        $ad = new Ad();
        $ad->title = $request->title;
        $ad->img = $request->img;
        $ad->content = $request->content;
        $ad->save();

        return redirect()->back();
    }

    public function editAd(Request $request){
        Ad::where('id',$request->id)->update(
            [
                'title' => $request->title,
                'img' => $request->img,
                'content' => $request->content
            ]
            );
            return redirect()->back();
    }

    //返回公告
    public function showAd(Request $request){
        if($request->id != null){
            $ad = ad::where('id',$request->id)->get();
            return response()->json($ad);
        }
        return response()->json(Ad::all()->toArray());
    }

    //删除公告
    public function rmAd(Request $request){
        Ad::where('id',$request->id)->delete();
        return redirect()->back();
    }

}
