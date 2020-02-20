<?php

namespace Nsiso;

use Illuminate\Http\Request;
use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\DB;
use Nsiso\Models\Ad;
use Nsiso\Models\Update;

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
        //按照时间顺序排列
        return response()->json(DB::table('ads')->orderBy('created_at','desc')->get()->toArray());
    }

    //删除公告
    public function rmAd(Request $request){
        Ad::where('id',$request->id)->delete();
        return redirect()->back();
    }
    //添加更新
    public function addUpdate(Request $request){
        $update = new Update();
        $update->version = $request->version;
        $update->update_files = $request->update_files;
        $update->delete_files = $request->delete_files;
        $update->save();
        return redirect()->back();
    }

    //删除更新
    public function rmUpdate(Request $request){
        Update::where('version',$request->version)->delete();
        return redirect()->back();
    }
    //显示更新
    public function showUpdate(){
        return json(DB::table('updates')->get()->toArray());
    }
}
