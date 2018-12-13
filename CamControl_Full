# unity中摄像机旋转、缩放、平移
```
using UnityEngine;
using System.Collections;
public class CamControl_Full : MonoBehaviour {
	public static CamControl_Full Ins;
	public enum Angle {
		followInching,//跟踪加缓动效果
		freeInching,//自由加缓动效果
		onlyFollow,//仅跟踪视角
		onlyFree,//仅自由视角
		none
	}
	Transform m_Transform;
	public Transform target;//相机所看目标点，若target==null，则相机为自由缓动视角，否则为跟踪缓动视角
	public Angle angle;//初始默认视角
	public float yMinLimit = -50.0f;//y最小角度视角限制
	public float yMaxLimit = 90.0f;//y最大角度视角限制
	public float xMinRangeLimit = float.MinValue;//x最小限制距离
	public float xMaxRangeLimit = float.MaxValue;//x最大限制距离
	public float yMinRangeLimit = float.MinValue;//y最小限制距离
	public float yMaxRangeLimit = float.MaxValue;//y最大限制距离
	public float zMinRangeLimit = float.MinValue;//z最小距离限制
	public float zMaxRangeLimit = float.MaxValue;//z最大距离限制
	public float minDistanceToTarget = 0.0f;//距离目标最小距离
	public float maxDistanceToTarget = 10.0f;//距离目标最大距离
	public float xSpeed = 200.0f;//x角度旋转速度
	public float ySpeed = 120.0f;//y角度旋转速度
	public float scaleSpeed = 4.0f;//缩放速度
	public float xMoveSpeed = 4.0f;//x方向平移速度
	public float yMoveSpeed = 4.0f;//y方向平移速度
	public float xCurrentAngle = 50.0f;//当前x角度
	public float yCurrentAngle = 25.0f;//当前y角度
	public float x, y;
	public float distance = 5.0f;//相机与目标点距离
	public float lerpSpeed = 5.0f;//差值速度（缓动速度）
	private float slowlySpeed = 2.0f;//缓动速度
	private float zCurrentScale = 0.0f;//当前缩放
	private float xCurrentScale = 0.0f;//当前横向平移
	private float yCurrentScale = 0.0f;//当前纵向平移


	public bool bSwitch=false;
	private Vector2 oldPosition1;
	private Vector2 oldPosition2;

	public bool bDebug = false;
	void Start() {
		if (!Ins) {
			Ins = this;
		}
		m_Transform = this.transform;
	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.F5)) {
			x = 0;
			y = 0;
		}
		switch (angle) {
			case Angle.followInching:
				if (target != null)
					FollowAngle(true);
				break;
			case Angle.freeInching:
				FreeAngle(true);
				break;
			case Angle.onlyFollow:
				if (target != null)
					FollowAngle(false);
				break;
			case Angle.onlyFree:
				FreeAngle(false);
				break;
			case Angle.none:
				break;
			default:
				break;
		}
	}
	/// <summary>
	/// 自由视角
	/// </summary>
	/// <param name="isSlowAction">是否缓动判断</param>
	void FreeAngle(bool isSlowAction) {
		#region 旋转
		if (Input.GetMouseButton(1)) {
			xCurrentAngle += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
			yCurrentAngle -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
			yCurrentAngle = ClampAngle(yCurrentAngle, yMinLimit, yMaxLimit);//y旋转角度限制
		}
		Quaternion rotationTo = Quaternion.Euler(yCurrentAngle, xCurrentAngle, 0);
		#endregion
		#region 缩放
		if (Input.GetAxis("Mouse ScrollWheel") != 0.0f) {
			zCurrentScale = Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;
		}
		#endregion
		#region 平移
		if (Input.GetMouseButton(2)) {
			xCurrentScale = -Input.GetAxis("Mouse X") * Time.deltaTime * xMoveSpeed;
			yCurrentScale = -Input.GetAxis("Mouse Y") * Time.deltaTime * yMoveSpeed;
			xCurrentScale = xCurrentScale > 1 ? 1 : xCurrentScale;
			yCurrentScale = yCurrentScale > 1 ? 1 : yCurrentScale;
			xCurrentScale = xCurrentScale < -1 ? -1 : xCurrentScale;
			yCurrentScale = yCurrentScale < -1 ? -1 : yCurrentScale;
		}
		#endregion
		if (isSlowAction)//带缓动效果
        {
			m_Transform.rotation = Quaternion.Lerp(m_Transform.rotation, rotationTo, Time.deltaTime * lerpSpeed);
			m_Transform.position = Vector3.Lerp(m_Transform.position, m_Transform.position + m_Transform.forward * (zCurrentScale > 0 ? ((zCurrentScale -= Time.deltaTime * slowlySpeed) < 0 ? zCurrentScale = 0 : zCurrentScale) : ((zCurrentScale += Time.deltaTime * slowlySpeed) > 0 ? zCurrentScale = 0 : zCurrentScale)) + m_Transform.right * (xCurrentScale > 0 ? ((xCurrentScale -= Time.deltaTime * slowlySpeed) < 0 ? xCurrentScale = 0 : xCurrentScale) : ((xCurrentScale += Time.deltaTime * slowlySpeed) > 0 ? xCurrentScale = 0 : xCurrentScale)) + m_Transform.up * (yCurrentScale > 0 ? ((yCurrentScale -= Time.deltaTime * slowlySpeed) < 0 ? yCurrentScale = 0 : yCurrentScale) : ((yCurrentScale += Time.deltaTime * slowlySpeed) > 0 ? yCurrentScale = 0 : yCurrentScale)), Time.deltaTime * lerpSpeed);
		}
		else//正常
        {
			Vector3 positionTo = rotationTo * new Vector3(xCurrentScale, yCurrentScale, zCurrentScale) + m_Transform.position;
			xCurrentScale = 0;
			yCurrentScale = 0;
			zCurrentScale = 0;
			m_Transform.rotation = rotationTo;
			m_Transform.position = positionTo;
		}
		m_Transform.position = new Vector3(Mathf.Clamp(m_Transform.position.x, xMinRangeLimit, xMaxRangeLimit), Mathf.Clamp(m_Transform.position.y, yMinRangeLimit, yMaxRangeLimit), Mathf.Clamp(m_Transform.position.z, zMinRangeLimit, zMaxRangeLimit));//自由模式区域限制
	}
	/// <summary>
	/// 跟踪视角
	/// </summary>
	/// <param name="isSlowAction">是否缓动判断</param>
	public void SwitchCameraFunction(){
		bSwitch=!bSwitch;
	}
	void FollowAngle(bool isSlowAction) {
		if (bDebug) {
			if (Input.GetMouseButton (1)) {
				xCurrentAngle += Input.GetAxis ("Mouse X") * xSpeed * Time.deltaTime;
				yCurrentAngle -= Input.GetAxis ("Mouse Y") * ySpeed * Time.deltaTime;
				yCurrentAngle = ClampAngle (yCurrentAngle, yMinLimit, yMaxLimit);//y旋转角度限制
			}
			if (Input.GetMouseButton(2)) {
				xCurrentScale = -Input.GetAxis("Mouse X") * Time.deltaTime * xMoveSpeed;
				yCurrentScale = -Input.GetAxis("Mouse Y") * Time.deltaTime * yMoveSpeed;
				x += xCurrentScale;
				x = Mathf.Clamp (x, xMinRangeLimit, xMaxRangeLimit);
				y += yCurrentScale;
				y = Mathf.Clamp (y, yMinRangeLimit, yMaxRangeLimit);
			}
			distance -= Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;
			distance = Mathf.Clamp(distance, minDistanceToTarget, maxDistanceToTarget);//缩放距离限制
			Quaternion rotationTo = Quaternion.Euler(yCurrentAngle, xCurrentAngle, 0);
			Vector3 positionTo = rotationTo * new Vector3(x, y, -distance) + target.position;
			if (isSlowAction)//带缓动效果
			{
				m_Transform.rotation = Quaternion.Lerp(m_Transform.rotation, rotationTo, Time.deltaTime * lerpSpeed);
				m_Transform.position = Vector3.Lerp(m_Transform.position, positionTo, Time.deltaTime * lerpSpeed);
			}
			else//正常
			{
				m_Transform.rotation = rotationTo;
				m_Transform.position = positionTo;
			}
		} else {
			if (bSwitch) {
				if (Input.touchCount == 1) {
					if (Input.GetTouch (0).phase == TouchPhase.Moved) {
						xCurrentAngle += Input.GetAxis ("Mouse X") * xSpeed * 0.02f;
						yCurrentAngle -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;
						yCurrentAngle = ClampAngle (yCurrentAngle, yMinLimit, yMaxLimit);//y旋转角度限制
					}
				}
			} else {
				if (Input.touchCount == 1) {
					if (Input.GetTouch (0).phase == TouchPhase.Moved) {
						xCurrentScale = -Input.GetAxis ("Mouse X") * 0.02f * xMoveSpeed;
						yCurrentScale = -Input.GetAxis ("Mouse Y") * 0.02f * yMoveSpeed;
						x += xCurrentScale;
						y += yCurrentScale;
					}
				}
			}
			if (Input.touchCount > 1) {
				if (Input.GetTouch (0).phase == TouchPhase.Moved || Input.GetTouch (1).phase == TouchPhase.Moved) {
					Vector3 tempPosition1 = Input.GetTouch (0).position;
					Vector3 tempPosition2 = Input.GetTouch (1).position;
					if (isEnlarge (oldPosition1, oldPosition2, tempPosition1, tempPosition2)) {
						distance -= scaleSpeed;
					} else {
						distance += scaleSpeed;
					}
					oldPosition1 = tempPosition1;
					oldPosition2 = tempPosition2;
				}
			}
			//distance -= Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;
			distance = Mathf.Clamp (distance, minDistanceToTarget, maxDistanceToTarget);//缩放距离限制
			Quaternion rotationTo = Quaternion.Euler (yCurrentAngle, xCurrentAngle, 0);
			Vector3 positionTo = rotationTo * new Vector3 (x, y, -distance) + target.position;
			if (isSlowAction) {//带缓动效果
				m_Transform.rotation = Quaternion.Lerp (m_Transform.rotation, rotationTo, Time.deltaTime * lerpSpeed);
				m_Transform.position = Vector3.Lerp (m_Transform.position, positionTo, Time.deltaTime * lerpSpeed);
			} else {//正常
				m_Transform.rotation = rotationTo;
				m_Transform.position = positionTo;
			}
		}
	}
	/// <summary>
	/// 限制视角旋转角度
	/// </summary>
	/// <param name="angle">当前角度</param>
	/// <param name="min">最小旋转角度</param>
	/// <param name="max">最大旋转角度</param>
	/// <returns></returns>
	float ClampAngle(float angle, float min, float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}

	//检测是否放大还是缩小
	public bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
	{
		float leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
		float leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
		if (leng1 < leng2)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
```
