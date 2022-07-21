 //Ray
Debug.DrawRay(rigid.position, dirVec * 1f, new Color(0,1,0));
Debug.Log(rigid.position);
Debug.Log(NetworkManager.instance.player.transform.position);

Debug.DrawRay(NetworkManager.instance.player.transform.position, new Vector3(50, 0, 0), new Color(0,1,0));
RaycastHit2D rayHit = Physics2D.Raycast(NetworkManager.instance.player.transform.position, new Vector3(50, 0, 0), LayerMask.GetMask("NoPassing"));
if(rayHit.collider != null) {
    scanObject = rayHit.collider.gameObject;
} else {
    scanObject = null;
}

if(h == 1) {
    // Debug.Log("오른쪽");
    Debug.DrawRay(NetworkManager.instance.player.transform.position, new Vector3(50, 0, 0), Color.red);
    RaycastHit2D rayHit = Physics2D.Raycast(NetworkManager.instance.player.transform.position, new Vector3(50, 0, 0), LayerMask.GetMask("NoPassing"));
    if(rayHit.collider != null) {
        // scanObject = rayHit.collider.gameObject;
        // canMove = true;
    } else {
        // scanObject = null;
        // canMove = false;
    }
}   else if(h == -1) {
    // Debug.Log("왼쪽");
    Debug.DrawRay(NetworkManager.instance.player.transform.position, new Vector3(-50, 0, 0), Color.red);
}   else if(v == 1) {
    Debug.Log("위쪽");
    Debug.DrawRay(NetworkManager.instance.player.transform.position, new Vector3(0, 50, 0), Color.red);
    RaycastHit2D rayHit = Physics2D.Raycast(NetworkManager.instance.player.transform.position, new Vector3(0, 50, 0), LayerMask.GetMask("NoPassing"));
    if(rayHit.collider != null) {
        scanObject = rayHit.collider.gameObject;
        // Debug.Log("찌르는 중");
        // canMove = true;
    } else {
        scanObject = null;
        // Debug.Log("찔림");
        // canMove = false;
    }
}   else if(v == -1) {
    // Debug.Log("아래쪽");
    Debug.DrawRay(NetworkManager.instance.player.transform.position, new Vector3(0, -50, 0), Color.red);
}
