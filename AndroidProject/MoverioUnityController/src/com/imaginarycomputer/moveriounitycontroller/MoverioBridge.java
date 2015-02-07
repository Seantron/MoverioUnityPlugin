package com.imaginarycomputer.moveriounitycontroller;


import android.os.Bundle;
import android.os.Looper;
import jp.epson.moverio.bt200.*;


import com.unity3d.player.UnityPlayerActivity;


public class MoverioBridge extends UnityPlayerActivity {
	
	private DisplayControl mDisplayControl = null;
	private SensorControl mSensorControl = null;
	private AudioControl mAudioControl = null;
	
	private boolean isMoverioDevice = false;
	
	private static MoverioBridge mInstance = null;
	
	public static MoverioBridge instance()
	{
		if(mInstance == null)
		{
			
			Looper.prepare();
			mInstance = new MoverioBridge();
			
		}
		
		return mInstance;
		
	}
	
	public MoverioBridge()
	{
		
	}
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
        
       
        
		super.onCreate(savedInstanceState);
		
		mDisplayControl = new DisplayControl(this);
		mSensorControl = new SensorControl(this);
		mAudioControl = new AudioControl(this);
		
		
	}
	
	public void onStop() {
    	if(isMoverioDevice)
    	{
    		mSensorControl.setMode(SensorControl.SENSOR_MODE_HEADSET);
        	mDisplayControl.setBacklight(20);
        	mDisplayControl.setMode(DisplayControl.DISPLAY_MODE_2D, false);
        	mAudioControl.setMute(false);
    	
    	}
    	
        super.onStop();

        /*
         * Check if the HOME key was pressed. If the HOME key was pressed then
         * the app will be killed either safely or quickly. Otherwise the user
         * or the app is navigating away from the activity so assume that the
         * HOME key will be pressed next unless a navigation event by the user
         * or the app occurs.
         */
        
    }
	
	public void SetMoverioDevice()
	{
		isMoverioDevice = true;
	}
	
	
	public void SetDisplay3D(boolean on)
	{
		if(on)
		{
			mDisplayControl.setMode(DisplayControl.DISPLAY_MODE_3D, true);
		} else {
			mDisplayControl.setMode(DisplayControl.DISPLAY_MODE_2D, false);
		}
	}
	
	public String SetSensorMode(int mode)
	{
		String msg = "";
		if(mode > -1 && mode < 2)
		{
			switch(mode)
			{
			case 0:
				mSensorControl.setMode(SensorControl.SENSOR_MODE_HEADSET);
				msg = "SUCCESS: SensorModeHeadset";
				break;
			case 1:
				mSensorControl.setMode(SensorControl.SENSOR_MODE_CONTROLLER);
				msg = "SUCCESS: SensorModeController";
				break;
			}
			
			msg = "SUCCESS";
		} else {
			msg = "ERROR: setSensorMode value out of range. Must be 0 or 1";
		}
		
		return msg;
		
	}
	
	public String SetDisplayBrightness(int brightness)
	{
		String msg = "";
		if(brightness > -1 && brightness < 21)
		{
			mDisplayControl.setBacklight(brightness);
			msg = "SUCCESS: setBackLight: "+ String.valueOf(brightness);
		} else {
			msg = "ERROR: setBackLight value out of range. Must be 0-20";
		}
		
		return msg;
	}
	
	public int GetDisplayBrightness()
	{
		int i = mDisplayControl.getBacklight();
		
		return i;
	}
	
	public void MuteDisplay(boolean on)
	{
		mDisplayControl.setMute(on);
	}
	
	public void MuteAudio(boolean on)
	{
		mAudioControl.setMute(on);
	}

}
