<?php

namespace Nsiso;

use Option;
use Parsedown;
use Nsiso\Models\Ad;

class Configuration
{
    public function render()
    {
        $path = plugin('nsiso')->getPath().'/README.md';
        $markdown = @file_get_contents($path);
        if (!$markdown) {
            $readme = "<p>无法加载 README.md</p>";
        } else {
            $readme = (new Parsedown())->text($markdown);
        }
        $ads = Ad::all();
        $adCount = $ads->count();
        return view('Nsiso::config', compact('readme','ads','adCount'));
    }
}
