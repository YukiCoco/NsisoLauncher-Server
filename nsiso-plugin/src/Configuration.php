<?php

namespace Nsiso;

use Option;
use Parsedown;
use Nsiso\Models\Ad;
use Nsiso\Models\Update;

class Configuration
{
    public function render()
    {
        $md5Form = Option::form('common', '防作弊配置', function($form) {
            $form->checkbox('ygg_enable_nsiso_check', 'nsiso 防作弊')
                ->label('开启 nsiso 防作弊');
            $form->textarea('md5_list','模组 md5 列表')->rows(5)->hint('使用工具获取模组的 md5 ,使用英文逗号隔开。');
        })->handle();
        $path = plugin('nsiso')->getPath().'/README.md';
        $markdown = @file_get_contents($path);
        if (!$markdown) {
            $readme = "<p>无法加载 README.md</p>";
        } else {
            $readme = (new Parsedown())->text($markdown);
        }
        $ads = Ad::all();
        $updates = Update::all();
        $adCount = $ads->count();
        return view('Nsiso::config', compact('readme','ads','adCount','md5Form','updates'));
    }
}
